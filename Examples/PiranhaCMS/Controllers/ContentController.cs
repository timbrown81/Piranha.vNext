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
using System.Web.Mvc;
using Piranha.AspNet.Cache;

namespace PiranhaCMS.Controllers
{
	/// <summary>
	/// Default controller for displaying content.
	/// </summary>
	public class ContentController : Piranha.AspNet.Mvc.ContentController
	{
		/// <summary>
		/// Gets the view for the current post.
		/// </summary>
		/// <returns>The view result</returns>
		public ActionResult Index() {
			var model = GetModel().WithComments();

			return View(model.View, model);
		}
	}
}