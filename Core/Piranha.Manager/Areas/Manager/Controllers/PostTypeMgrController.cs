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
using System.Web.Mvc;
using Piranha.Manager.Models.PostType;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing post types.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix="manager")]
	public class PostTypeMgrController : ManagerController
	{
		/// <summary>
		/// Gets the list of the currently available post types.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("posttypes")]
		public ActionResult List() {
			return View(ListModel.Get());
		}

		/// <summary>
		/// Gets the edit view for a new or existing post type.
		/// </summary>
		/// <param name="id">The optional id</param>
		/// <returns>The view result</returns>
		[Route("posttype/edit/{id:Guid?}")]
		public ActionResult Edit(Guid? id = null) {
			if (id.HasValue) {
				ViewBag.Title = Piranha.Manager.Resources.PostType.EditTitle;
				return View(EditModel.GetById(api, id.Value));
			} else {
				ViewBag.Title = Piranha.Manager.Resources.PostType.AddTitle;
				return View(new EditModel());
			}
		}

		/// <summary>
		/// Saves the given post type.
		/// </summary>
		/// <param name="model">The post type</param>
		/// <returns>The view result</returns>
		[HttpPost]
		[Route("posttype/save")]
		[ValidateAntiForgeryToken]
		public ActionResult Save(EditModel model) {
			if (ModelState.IsValid) {
				model.Save(api);
				IsSaved = true;
				return RedirectToAction("edit", new { id = model.Id });
			}
			if (model.Id.HasValue)
				ViewBag.Title = Piranha.Manager.Resources.PostType.EditTitle;
			else ViewBag.Title = Piranha.Manager.Resources.PostType.AddTitle;

			return View("Edit", model);
		}

		/// <summary>
		/// Deletes the post type with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The redirect result</returns>
		[Route("posttype/delete/{id:Guid}")]
		public ActionResult Delete(Guid id) {
			api.PostTypes.Remove(id);
			api.SaveChanges();

			return RedirectToAction("List");
		}
	}
}