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
using System.Web;
using System.Web.Mvc;
using Piranha.Manager.Models.Template;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing templates.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix = "manager")]
    public class TemplateMgrController : ManagerController
    {
		/// <summary>
		/// Gets a list of the currently available templates.
		/// </summary>
		/// <returns>The template list</returns>
		[Route("templates")]
		public ActionResult List() {
			return View();
		}

		/// <summary>
		/// Gets the available templates.
		/// </summary>
		/// <returns>The templates</returns>
		[Route("templates/get")]
		public ActionResult Get() {
			return JsonData(true, Mapper.Map<IEnumerable<Piranha.Models.Template>, IEnumerable<ListItem>>(api.Templates.Get()));
		}
    }
}