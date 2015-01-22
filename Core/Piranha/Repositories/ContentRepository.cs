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
	public sealed class ContentRepository : Repository<Models.Content>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal ContentRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Adds a new or updated model to the api.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.Content model) {
			// Ensure slug
			if (String.IsNullOrWhiteSpace(model.Slug))
				model.Slug = Utils.GenerateSlug(model.Title);

			// Add model
			base.Add(model);
		}

		/// <summary>
		/// Orders the content query by name.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.Content> Order(IQueryable<Models.Content> query) {
			return query.OrderBy(c => c.Published);
		}

		/// <summary>
		/// Maps the source model to the destination.
		/// </summary>
		/// <param name="model">The source model</param>
		protected override Models.Content FromDb(Models.Content model) {
			// Sort categories
			model.Categories = model.Categories.OrderBy(c => c.Title).ToList();

			// Sort rows
			model.Rows = model.Rows.OrderBy(r => r.SortOrder).ToList();

			// Sort blocks
			foreach (var row in model.Rows)
				row.Blocks = row.Blocks.OrderBy(b => b.SortOrder).ToList();
			return model;
		}
	}
}