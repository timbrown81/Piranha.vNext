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
	public sealed class ParamRepository : Repository<Models.Param>
	{
		/// <summary>
		/// Default internal constructor.
		/// </summary>
		/// <param name="session">The current session</param>
		internal ParamRepository(Data.ISession session) : base(session) { }

		/// <summary>
		/// Gets the model identified by the given id. 
		/// </summary>
		/// <remarks>
		/// This method uses the configured cache for performance.
		/// </remarks>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public override Models.Param GetSingle(Guid id) {
			var model = App.ModelCache.GetById<Models.Param>(id);

			if (model == null) {
				model = base.GetSingle(id);

				if (model != null)
					App.ModelCache.Add(model);
			}
			return model;
		}

		/// <summary>
		/// Gets the model identified by the given name. 
		/// </summary>
		/// <remarks>
		/// This method uses the configured cache for performance.
		/// </remarks>
		/// <param name="name">The unique name</param>
		/// <returns>The model</returns>
		public Models.Param GetSingle(string name) {
			var model = App.ModelCache.GetByKey<Models.Param>(name);

			if (model == null) {
				model = base.GetSingle(where: p => p.Name == name);

				if (model != null)
					App.ModelCache.Add(model);
			}
			return model;
		}

		/// <summary>
		/// Orders the category query by name.
		/// </summary>
		/// <param name="query">The query</param>
		/// <returns>The ordered query</returns>
		protected override IQueryable<Models.Param> Order(IQueryable<Models.Param> query) {
			return query.OrderBy(b => b.Name);
		}
	}
}