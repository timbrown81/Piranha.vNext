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

namespace Piranha.Repositories
{
	public sealed class PostTypeRepository : Repository<Models.PostType>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal PostTypeRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Gets the model identified by the given id. 
		/// </summary>
		/// <remarks>
		/// This method uses the configured cache for performance.
		/// </remarks>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public override Models.PostType GetSingle(Guid id) {
			var model = App.ModelCache.GetById<Models.PostType>(id);

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
		/// <returns>The model</returns>
		public Models.PostType GetSingle(string slug) {
			var model = App.ModelCache.GetByKey<Models.PostType>(slug);

			if (model == null) {
				model = base.GetSingle(where: p => p.Slug == slug);

				if (model != null)
					App.ModelCache.Add(model);
			}
			return model;
		}

		/// <summary>
		/// Adds a new or updated model to the api.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.PostType model) {
			// Ensure slug
			if (String.IsNullOrWhiteSpace(model.Slug))
				model.Slug = Utils.GenerateSlug(model.Name);

			// Add model
			base.Add(model);
		}

		/// <summary>
		/// Orders the post type query by name.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.PostType> Order(IQueryable<Models.PostType> query) {
			return query.OrderBy(t => t.Name);
		}
	}
}