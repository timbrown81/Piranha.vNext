/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Piranha.Manager.Models.Category;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing categories.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix = "manager")]
    public class CategoryMgrController : ManagerController
    {
		/// <summary>
		/// Gets a list of the currently available categories.
		/// </summary>
		/// <returns>The category list</returns>
		[Route("categories")]
		public ActionResult List() {
			return View();
		}

		/// <summary>
		/// Gets the categories.
		/// </summary>
		/// <returns>The categories</returns>
		[Route("categories/get")]
		public ActionResult Get() {
			return JsonData(true, Mapper.Map<IEnumerable<Piranha.Models.Category>, IEnumerable<ListItem>>(api.Categories.Get()));
		}

		/// <summary>
		/// Gets the category with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The category</returns>
		[Route("category/get/{id:Guid}")]
		public ActionResult GetSingle(Guid id) {
			return JsonData(true, EditModel.GetById(api, id));
		}

		/// <summary>
		/// Saves the given category model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The redirect result</returns>
		[HttpPost]
		[Route("category/save")]
		[ValidateInput(false)]
		public ActionResult Save(EditModel model) {
			if (ModelState.IsValid) {
				model.Save(api);
				var items = Mapper.Map<IEnumerable<Piranha.Models.Category>, IEnumerable<ListItem>>(api.Categories.Get());
				var current = items.Where(c => c.Id == model.Id).SingleOrDefault();
				if (current != null) {
					current.Saved = true;
				}
				return JsonData(true, items);
			}
			return JsonData(false);
		}

		/// <summary>
		/// Deletes the category with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The redirect result</returns>
		[Route("category/delete/{id:Guid}")]
		public ActionResult Delete(Guid id) {
			api.Categories.Remove(id);
			api.SaveChanges();

			return JsonData(true, Mapper.Map<IEnumerable<Piranha.Models.Category>, IEnumerable<ListItem>>(api.Categories.Get()));
		}
    }
}