/*
 * Piranha CMS
 * Copyright (c) 2014, Håkan Edling, All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Piranha.Repositories
{
	public sealed class RatingRepository
	{
		#region Members
		/// <summary>
		/// The private storage session.
		/// </summary>
		private Data.ISession session;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="session">The current session to use</param>
		internal RatingRepository(Data.ISession session) {
			this.session = session;
		}

		/// <summary>
		/// Gets the ratings matching the given where expression.
		/// </summary>
		/// <param name="where">The optional where expression</param>
		/// <returns>The matching ratings</returns>
		public IEnumerable<Models.Rating> Get(Expression<Func<Models.Rating, bool>> where = null) {
			return session.Get<Models.Rating>(where);
		}

		/// <summary>
		/// Adds a rating of the given type.
		/// </summary>
		/// <param name="type">The rating type</param>
		/// <param name="modelId">The unique model type</param>
		/// <param name="userId">The user id</param>
		public void AddRating(Models.RatingType type, Guid modelId, string userId) {
			var rating = session.Get<Models.Rating>(where: r => r.Type == type && r.ModelId == modelId && r.UserId == userId).SingleOrDefault();

			if (rating == null) {
				session.Add<Models.Rating>(new Models.Rating() {
					Type = type,
					ModelId = modelId,
					UserId = userId
				});
			}
		}

		/// <summary>
		/// Removes a rating of the given type.
		/// </summary>
		/// <param name="type">The rating type</param>
		/// <param name="modelId">The unique model type</param>
		/// <param name="userId">The user if</param>
		public void RemoveRating(Models.RatingType type, Guid modelId, string userId) {
			var rating = session.Get<Models.Rating>(where: r => r.Type == type && r.ModelId == modelId && r.UserId == userId).SingleOrDefault();

			if (rating != null)
				session.Remove<Models.Rating>(rating);
		}
	}
}
