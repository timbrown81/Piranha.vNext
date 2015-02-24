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
using Piranha.Manager.Models.Author;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing blocks.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix = "manager")]
	public class AuthorMgrController : ManagerController
	{
		/// <summary>
		/// Gets a list of the currently available authors
		/// </summary>
		/// <returns>The author list</returns>
		[Route("authors")]
		public ActionResult List() {
			return View();
		}

		/// <summary>
		/// Gets the authors.
		/// </summary>
		/// <returns>The authors</returns>
		[Route("authors/get")]
		public ActionResult Get() {
			return JsonData(true, GetAuthors());
		}

		/// <summary>
		/// Gets the author with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The author</returns>
		[Route("author/get/{id:Guid}")]
		public ActionResult GetSingle(Guid id) {
			return JsonData(true, EditModel.GetById(api, id));
		}

		/// <summary>
		/// Saves the given post model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The redirect result</returns>
		[HttpPost]
		[Route("author/save")]
		[ValidateInput(false)]
		public ActionResult Save(EditModel model) {
			if (ModelState.IsValid) {
				model.Save(api);
				var items = GetAuthors();
				var current = items.Where(c => c.Id == model.Id).SingleOrDefault();
				if (current != null) {
					current.Saved = true;
				}
				return JsonData(true, items);
			}
			return JsonData(false);
		}

		/// <summary>
		/// Deletes the block with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The redirect result</returns>
		[Route("author/delete/{id:Guid}")]
		public ActionResult Delete(Guid id) {
			api.Authors.Remove(id);
			api.SaveChanges();

			return JsonData(true, Mapper.Map<IEnumerable<Piranha.Models.Author>, IEnumerable<ListItem>>(api.Authors.Get()));
		}

		#region Private methods
		/// <summary>
		/// Gets the author list.
		/// </summary>
		/// <returns>The list of authors</returns>
		private IEnumerable<ListItem> GetAuthors() {
			var authors = Mapper.Map<IEnumerable<Piranha.Models.Author>, IEnumerable<ListItem>>(api.Authors.Get());
			var ui = new Client.Helpers.UIHelper();

			foreach (var item in authors) {
				item.GravatarUrl = ui.GravatarUrl(item.Email, 40);
			}
			return authors;
		}
		#endregion
	}
}