/*
 * Copyright (c) 2014 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Piranha.Data;
using Raven.Client;
using Raven.Client.Linq;

namespace Piranha.RavenDb
{
	/// <summary>
	/// Interface defining the different methods that should be provided
	/// by an open session to a document store.
	/// </summary>
	public class Session : ISession
	{
		#region Members
		/// <summary>
		/// The document session.
		/// </summary>
		private readonly IDocumentSession session;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="session">The session</param>
		public Session(IDocumentSession session) {
			this.session = session;
		}

		/// <summary>
		/// Gets the document identified by the given id.
		/// </summary>
		/// <typeparam name="T">The document type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The document</returns>
		public T GetSingle<T>(Guid id) where T : class, IModel {
			T model = null;

			if (model is Models.Post) {
				model = session
					.Include<Models.Post, Models.Author>(p => p.AuthorId)
					.Include<Models.PostType>(p => p.TypeId)
					.Load<T>(id);
			} else if (model is Models.Page) {
				model = session
					.Include<Models.Page, Models.Author>(p => p.AuthorId)
					.Include<Models.PageType>(p => p.TypeId)
					.Load<T>(id);
			} else {
				model = session.Load<T>(id);
			}

			return Process(model);
		}

		/// <summary>
		/// Gets the documents matching the given expression.
		/// </summary>
		/// <typeparam name="T">The document type</typeparam>
		/// <param name="where">The optional where expression</param>
		/// <returns>The matching documents</returns>
		public IEnumerable<T> Get<T>(Expression<Func<T, bool>> where = null, int? limit = null, Func<IQueryable<T>, IQueryable<T>> order = null) 
			where T : class, IModel 
		{
			IQueryable<T> query = session.Query<T>();

			if (typeof(T) == typeof(Models.Page)) {
				query = ((IRavenQueryable<T>)query)
					.Customize(c => c.Include<Models.Page, Models.Author>(p => p.AuthorId))
					.Customize(c => c.Include<Models.Page, Models.PageType>(p => p.TypeId));
			} else if (typeof(T) == typeof(Models.Post)) {
				query = ((IRavenQueryable<T>)query)
					.Customize(c => c.Include<Models.Post, Models.Author>(p => p.AuthorId))
					.Customize(c => c.Include<Models.Post, Models.PostType>(p => p.TypeId));
			}
	
			// IQueryable<T> query = session.Query<T>();

			if (where != null)
				query = query.Where(where);
			if (order != null)
				query = order(query);
			if (limit.HasValue)
				query = query.Take(limit.Value);
			return Process(query.AsEnumerable());
		}

		/// <summary>
		/// Adds the given document to the session.
		/// </summary>
		/// <typeparam name="T">The document type</typeparam>
		/// <param name="document">The document</param>
		public void Add<T>(T document) where T : class, IModel {
			session.Store(document);
		}

		/// <summary>
		/// Removes the given document from the session.
		/// </summary>
		/// <typeparam name="T">The document type</typeparam>
		/// <param name="document">The document</param>
		public void Remove<T>(T document) where T : class, IModel {
			session.Delete<T>(document);
		}

		/// <summary>
		/// Saves the changes made to the current session.
		/// </summary>
		public void SaveChanges() {
			session.SaveChanges();
		}

		/// <summary>
		/// Disposes the session.
		/// </summary>
		public void Dispose() {
			session.Dispose();
			GC.SuppressFinalize(this);
		}

		#region Private methods
		/// <summary>
		/// Processes the loaded documents.
		/// </summary>
		/// <typeparam name="T">The document type</typeparam>
		/// <param name="documents">The documents</param>
		/// <returns>The processed documents</returns>
		private IEnumerable<T> Process<T>(IEnumerable<T> documents) {
			foreach (var document in documents)
				Process(document);
			return documents;
		}

		/// <summary>
		/// Processes the loaded document and loads additional resources.
		/// </summary>
		/// <typeparam name="T">The document type</typeparam>
		/// <param name="document">The document to process</param>
		/// <returns>The processed document</returns>
		private T Process<T>(T document) {
			if (document is Models.Post) {
				var model = (Models.Post)(object)document;

				model.Author = session.Load<Models.Author>(model.AuthorId);
				model.Type = session.Load<Models.PostType>(model.TypeId);
			} else if (document is Models.Page) {
				var model = (Models.Page)(object)document;

				model.Author = session.Load<Models.Author>(model.AuthorId);
				model.Type = session.Load<Models.PageType>(model.TypeId);
			}
			return document;
		}
		#endregion
	}
}
