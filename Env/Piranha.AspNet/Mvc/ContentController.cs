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
	/// Base controller for a single post.
	/// </summary>
	public abstract class ContentController : Controller
	{
		#region Properties
		/// <summary>
		/// Gets the currently requested content id.
		/// </summary>
		private Guid ContentId { get; set; }
		#endregion

		/// <summary>
		/// Gets the model for the currently requested content.
		/// </summary>
		/// <returns>The model</returns>
		protected ContentModel GetModel() {
			return ContentModel.GetById(ContentId);
		}

		/// <summary>
		/// Gets the model for the currently requested content.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The model</returns>
		protected ContentModel GetModel<T>() where T : ContentModel {
			return ContentModel.GetById<T>(ContentId);
		}

		/// <summary>
		/// Initializes the controller.
		/// </summary>
		/// <param name="filterContext">The current filter context</param>
		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			ContentId = new Guid(Request["id"]);

			base.OnActionExecuting(filterContext);
		}
	}
}
