/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Piranha.Repositories
{
	public sealed class PostRepository : Repository<Models.Post>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal PostRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Gets the model identified by the given id. 
		/// </summary>
		/// <remarks>
		/// This method uses the configured cache for performance.
		/// </remarks>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public override Models.Post GetSingle(Guid id) {
			var model = App.ModelCache.GetById<Models.Post>(id);

			if (model == null) {
				model = base.GetSingle(id);

				if (model != null)
					App.ModelCache.Add(model);
			}
			return model;
		}

		/// <summary>
		/// Gets the model identified by the given slug. 
		/// </summary>
		/// <remarks>
		/// This method uses the configured cache for performance.
		/// </remarks>
		/// <param name="slug">The unique slug</param>
		/// <param name="typeId">The unique post type id</param>
		/// <returns>The model</returns>
		public Models.Post GetSingle(string slug, Guid typeId) {
			var model = App.ModelCache.GetByKey<Models.Post>(typeId.ToString() + "_" + slug);

			if (model == null) {
				model = base.GetSingle(where: p => p.Slug == slug && p.TypeId == typeId);

				if (model != null)
					App.ModelCache.Add(model);
			}
			return model;
		}

		/// <summary>
		/// Adds a new or updated model to the api.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.Post model) {
			// Ensure slug
			if (String.IsNullOrWhiteSpace(model.Slug))
				model.Slug = Utils.GenerateSlug(model.Title);

			// Add model
			base.Add(model);
		}

		/// <summary>
		/// Orders the post query by published date.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.Post> Order(IQueryable<Models.Post> query) {
			return query.OrderByDescending(p => p.Published);
		}

		/// <summary>
		/// Maps the source model to the destination.
		/// </summary>
		/// <param name="model">The source model</param>
		protected override Models.Post FromDb(Models.Post model) {
			model.Categories = model.Categories.OrderBy(c => c.Title).ToList();

			return base.FromDb(model);
		}
	}
}