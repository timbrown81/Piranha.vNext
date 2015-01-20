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
using Piranha.Manager.Models.PageType;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing page types.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix="manager")]
	public class PageTypeMgrController : ManagerController
	{
		/// <summary>
		/// Gets the list of the currently available page types.
		/// </summary>
		/// <returns>The view result</returns>
		[Route("pagetypes")]
		public ActionResult List() {
			return View(ListModel.Get());
		}

		/// <summary>
		/// Gets an edit model for an existing page type or
		/// creates a new one.
		/// </summary>
		/// <param name="id">The optional id</param>
		/// <returns>The edit model</returns>
		[Route("pagetype/get/{id:Guid?}")]
		public ActionResult GetSingle(Guid? id) {
			if (id.HasValue)
				return JsonData(true, EditModel.GetById(api, id.Value));
			return JsonData(true, new EditModel());
		}

		/// <summary>
		/// Gets the edit view for a new or existing page type.
		/// </summary>
		/// <param name="id">The optional id</param>
		/// <returns>The view result</returns>
		[Route("pagetype/edit/{id:Guid?}")]
		public ActionResult Edit(Guid? id = null) {
			if (id.HasValue) {
				ViewBag.Title = Piranha.Manager.Resources.PageType.EditTitle;
				return View(id);
			} else {
				ViewBag.Title = Piranha.Manager.Resources.PageType.AddTitle;
				return View(id);
			}
		}

		/// <summary>
		/// Saves the given page type.
		/// </summary>
		/// <param name="model">The page type</param>
		/// <returns>The view result</returns>
		[HttpPost]
		[Route("pagetype/save")]
		public ActionResult Save(EditModel model) {
			if (ModelState.IsValid) {
				model.Save(api);

				return JsonData(true, model);
			}
			return JsonData(false);
		}

		/// <summary>
		/// Deletes the page type with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The redirect result</returns>
		[Route("pagetype/delete/{id:Guid}")]
		public ActionResult Delete(Guid id) {
			api.PageTypes.Remove(id);
			api.SaveChanges();

			return RedirectToAction("List");
		}
	}
}