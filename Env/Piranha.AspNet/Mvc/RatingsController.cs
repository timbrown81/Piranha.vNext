/*
 * Copyright (c) 2014 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;
using System.Linq;
using System.Web.Mvc;
using Piranha.Client.Models;

namespace Piranha.AspNet.Mvc
{
	/// <summary>
	/// Base controller for managing ratings.
	/// </summary>
	public class RatingsController : Controller
	{
		/// <summary>
		/// Adds a dislike to the specified model.
		/// </summary>
		/// <param name="id">The id of the model</param>
		/// <returns>If the action was successful or not</returns>
		[HttpGet]
		public virtual ActionResult AddDislike(string id) {
			return AddRating(Models.RatingType.Dislike, new Guid(id));
		}

		/// <summary>
		/// Adds a like to the specified model.
		/// </summary>
		/// <param name="id">The id of the model</param>
		/// <returns>If the action was successful or not</returns>
		[HttpGet]
		public virtual ActionResult AddLike(string id) {
			return AddRating(Models.RatingType.Like, new Guid(id));
		}

		/// <summary>
		/// Adds a star rating to the specified model.
		/// </summary>
		/// <param name="id">The id of the model</param>
		/// <returns>If the action was successful or not</returns>
		[HttpGet]
		public virtual ActionResult AddStar(string id) {
			return AddRating(Models.RatingType.Star, new Guid(id));
		}

		/// <summary>
		/// Adds a dislike to the specified model.
		/// </summary>
		/// <param name="id">The id of the model</param>
		/// <returns>If the action was successful or not</returns>
		[HttpGet]
		public virtual ActionResult RemoveDislike(string id) {
			return RemoveRating(Models.RatingType.Dislike, new Guid(id));
		}

		/// <summary>
		/// Adds a like to the specified model.
		/// </summary>
		/// <param name="id">The id of the model</param>
		/// <returns>If the action was successful or not</returns>
		[HttpGet]
		public virtual ActionResult RemoveLike(string id) {
			return RemoveRating(Models.RatingType.Like, new Guid(id));
		}

		/// <summary>
		/// Adds a star rating to the specified model.
		/// </summary>
		/// <param name="id">The id of the model</param>
		/// <returns>If the action was successful or not</returns>
		[HttpGet]
		public virtual ActionResult RemoveStar(string id) {
			return RemoveRating(Models.RatingType.Star, new Guid(id));
		}

		#region Private methods
		/// <summary>
		/// Adds a rating.
		/// </summary>
		/// <param name="type">The rating type</param>
		/// <param name="id">The model id</param>
		/// <returns>The result</returns>
		private ActionResult AddRating(Models.RatingType type, Guid id) {
			if (App.Security.IsAuthenticated()) {
				using (var api = new Api()) {
					api.Ratings.AddRating(type, id, App.Security.GetUserId());
					return Json(new {
						Success = true,
						Data = Client.Models.RatingsModel.GetByModelId(api, id)
					}, JsonRequestBehavior.AllowGet);
				}
			}
			return Json(new {
				Success = false
			}, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Removes a rating.
		/// </summary>
		/// <param name="type">The rating type</param>
		/// <param name="id">The model id</param>
		/// <returns>The result</returns>
		private ActionResult RemoveRating(Models.RatingType type, Guid id) {
			if (App.Security.IsAuthenticated()) {
				using (var api = new Api()) {
					api.Ratings.RemoveRating(type, id, App.Security.GetUserId());
					return Json(new {
						Success = true,
						Data = Client.Models.RatingsModel.GetByModelId(api, id)
					}, JsonRequestBehavior.AllowGet);
				}
			}
			return Json(new {
				Success = false
			}, JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}
