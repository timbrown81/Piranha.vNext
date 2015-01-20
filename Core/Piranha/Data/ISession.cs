/*
 * Copyright (c) 2014-2015 Håkan Edling
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

namespace Piranha.Data
{
	/// <summary>
	/// Interface defining the different methods that should be provided
	/// by an open session to a model store.
	/// </summary>
	public interface ISession : IDisposable
	{
		/// <summary>
		/// Gets the model identified by the given id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		T GetSingle<T>(Guid id) where T : class, Data.IModel;

		/// <summary>
		/// Gets the models matching the given expression.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="where">The optional where expression</param>
		/// <returns>The matching models</returns>
		IEnumerable<T> Get<T>(Expression<Func<T, bool>> where = null, int? limit = null, Func<IQueryable<T>, IQueryable<T>> order = null) where T : class, Data.IModel;

		/// <summary>
		/// Adds the given model to the session.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="model">The model</param>
		void Add<T>(T model) where T : class, Data.IModel;

		/// <summary>
		/// Removes the given model from the session.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="model">The model</param>
		void Remove<T>(T model) where T : class, Data.IModel;

		/// <summary>
		/// Saves the changes made to the current session.
		/// </summary>
		void SaveChanges();
	}
}
