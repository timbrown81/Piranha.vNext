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
	public sealed class TemplateRepository : Repository<Models.Template>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal TemplateRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Adds a new model to the current session.
		/// </summary>
		/// <param name="model">The model</param>
		public override void Add(Models.Template model) {
			// Ensure id
			if (model.Id == Guid.Empty)
				model.Id = Guid.NewGuid();

			// Ensure field id, foreign keys & sort order
			for (var n = 0; n < model.Fields.Count; n++) {
				var field = model.Fields[n];

				if (field.Id == Guid.Empty)
					field.Id = Guid.NewGuid();
				field.TemplateId = model.Id;
				field.SortOrder = n + 1;
			}
			base.Add(model);
		}

		/// <summary>
		/// Maps the source model to the destination.
		/// </summary>
		/// <param name="model">The source model</param>
		protected override Models.Template FromDb(Models.Template model) {
			// Sort the fields
			model.Fields = model.Fields.OrderBy(f => f.SortOrder).ToStateList();

			// Return the model
			return model;
		}

		/// <summary>
		/// Orders the template query by name.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.Template> Order(IQueryable<Models.Template> query) {
			return query.OrderBy(t => t.Name);
		}
	}
}