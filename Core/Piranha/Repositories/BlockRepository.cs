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
	public sealed class BlockRepository : Repository<Models.Block>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal BlockRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Gets the model identified by the given id. 
		/// </summary>
		/// <remarks>
		/// This method uses the configured cache for performance.
		/// </remarks>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public override Models.Block GetSingle(Guid id) {
			var model = App.ModelCache.GetById<Models.Block>(id);

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
		public Models.Block GetSingle(string slug) {
			var model = App.ModelCache.GetByKey<Models.Block>(slug);

			if (model == null) {
				model = base.GetSingle(where: b => b.Slug == slug);

				if (model != null)
					App.ModelCache.Add(model);
			}
			return model;
		}

		/// <summary>
		/// Adds a new or updated model to the api.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.Block model) {
			// Ensure slug
			if (String.IsNullOrWhiteSpace(model.Slug))
				model.Slug = Utils.GenerateSlug(model.Name);

			// Add model
			base.Add(model);
		}

		/// <summary>
		/// Orders the block query by name.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.Block> Order(IQueryable<Models.Block> query) {
			return query.OrderBy(c => c.Name);
		}
	}
}