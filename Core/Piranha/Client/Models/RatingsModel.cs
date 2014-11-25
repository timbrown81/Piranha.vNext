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
using Piranha.Models;

namespace Piranha.Client.Models
{
	/// <summary>
	/// Application ratings model.
	/// </summary>
	public class RatingsModel
	{
		#region Inner classes
		/// <summary>
		/// An item in the different rating lists.
		/// </summary>
		public class RatingsItem
		{
			/// <summary>
			/// Gets/sets the id of the user who made the rating.
			/// </summary>
			public string UserId { get; set; }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the available star ratings.
		/// </summary>
		public IList<RatingsItem> Stars { get; set; }

		/// <summary>
		/// Gets/sets the available likes.
		/// </summary>
		public IList<RatingsItem> Likes { get; set; }

		/// <summary>
		/// Gets/sets the available dislikes.
		/// </summary>
		public IList<RatingsItem> Dislikes { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public RatingsModel() {
			Stars = new List<RatingsItem>();
			Likes = new List<RatingsItem>();
			Dislikes = new List<RatingsItem>();
		}

		/// <summary>
		/// Gets the ratings model for the specified model.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="id">The model id</param>
		/// <returns>The available ratings</returns>
		public static RatingsModel GetByModelId(Api api, Guid id) {
			var ratings = api.Ratings.Get(where: r => r.ModelId == id);
			var model = new RatingsModel();

			foreach (var rating in ratings) {
				if (rating.Type == RatingType.Star)
					model.Stars.Add(new RatingsItem() { UserId = rating.UserId });
				else if (rating.Type == RatingType.Like)
					model.Likes.Add(new RatingsItem() { UserId = rating.UserId });
				else if (rating.Type == RatingType.Dislike)
					model.Dislikes.Add(new RatingsItem() { UserId = rating.UserId });
			}
			return model;
		}
	}
}
