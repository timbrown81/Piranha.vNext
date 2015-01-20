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
	public sealed class PageTypeRepository : Repository<Models.PageType>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal PageTypeRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Adds a new or updated model to the api.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.PageType model) {
			// Ensure slug
			if (String.IsNullOrWhiteSpace(model.Slug))
				model.Slug = Utils.GenerateSlug(model.Name);

			// Add model
			base.Add(model);
		}

		/// <summary>
		/// Maps the source model to the destination.
		/// </summary>
		/// <param name="model">The source model</param>
		protected override Models.PageType FromDb(Models.PageType model) {
			model.Regions = model.Regions.OrderBy(r => r.Order).ToStateList();

			return model;
		}

		/// <summary>
		/// Orders the page type query by name.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.PageType> Order(IQueryable<Models.PageType> query) {
			return query.OrderBy(t => t.Name);
		}
	}
}