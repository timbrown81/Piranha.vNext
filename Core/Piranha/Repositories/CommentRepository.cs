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
	public sealed class CommentRepository : Repository<Models.Comment>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal CommentRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Adds a new or updated model to the api.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.Comment model) {
			// Strip all HTML-tags from comments
			model.Body = model.Body.StripHtml();

			// Add model
			base.Add(model);
		}

		/// <summary>
		/// Orders the comment query by created date.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.Comment> Order(IQueryable<Models.Comment> query) {
			return query.OrderBy(c => c.Created);
		}
	}
}