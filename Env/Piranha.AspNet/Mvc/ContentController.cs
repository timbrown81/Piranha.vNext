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
using Piranha.Models;

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
		/// Gets the model for the currently requested post.
		/// </summary>
		/// <returns>The model</returns>
		protected Content GetModel() {
			using (var api = new Api()) {
				return api.Content.GetSingle(ContentId);
			}
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
