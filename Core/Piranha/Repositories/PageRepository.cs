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
	public sealed class PageRepository : Repository<Models.Page>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal PageRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Adds a new or updated model to the api.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.Page model) {
			// Ensure slug
			if (String.IsNullOrWhiteSpace(model.Slug))
				model.Slug = Utils.GenerateSlug(model.Title);

			// Add model
			base.Add(model);
		}

		/// <summary>
		/// Orders the page query by published date.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.Page> Order(IQueryable<Models.Page> query) {
			return query.OrderByDescending(t => t.Published);
		}
	}
}