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
using Piranha.Manager.Models.Alias;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing aliases.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix = "manager")]
    public class AliasMgrController : ManagerController
    {
		/// <summary>
		/// Gets a list of the currently available aliases.
		/// </summary>
		/// <returns>The alias list</returns>
		[Route("aliases")]
		public ActionResult List() {
			return View();
		}

		/// <summary>
		/// Gets the aliases.
		/// </summary>
		/// <returns>The aliases</returns>
		[Route("aliases/get")]
		public ActionResult Get() {
			return JsonData(true, Mapper.Map<IEnumerable<Piranha.Models.Alias>, IEnumerable<ListItem>>(api.Aliases.Get()));
		}

		/// <summary>
		/// Gets the alias with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The alias</returns>
		[Route("alias/get/{id:Guid}")]
		public ActionResult GetSingle(Guid id) {
			return JsonData(true, EditModel.GetById(api, id));
		}

		/// <summary>
		/// Saves the given post model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The redirect result</returns>
		[HttpPost]
		[Route("alias/save")]
		[ValidateInput(false)]
		public ActionResult Save(EditModel model) {
			if (ModelState.IsValid) {
				model.Save(api);
				var items = Mapper.Map<IEnumerable<Piranha.Models.Alias>, IEnumerable<ListItem>>(api.Aliases.Get());
				var current = items.Where(c => c.Id == model.Id).SingleOrDefault();
				if (current != null) {
					current.Saved = true;
				}
				return JsonData(true, items);
			}
			return JsonData(false);
		}

		/// <summary>
		/// Deletes the alias with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The redirect result</returns>
		[Route("alias/delete/{id:Guid}")]
		public ActionResult Delete(Guid id) {
			api.Aliases.Remove(id);
			api.SaveChanges();

			return JsonData(true, Mapper.Map<IEnumerable<Piranha.Models.Alias>, IEnumerable<ListItem>>(api.Aliases.Get()));
		}
    }
}