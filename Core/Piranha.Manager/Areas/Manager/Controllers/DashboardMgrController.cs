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

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// The main entry point for the manager.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix = "manager")]
    public class DashboardMgrController : ManagerController
    {
		/// <summary>
		/// Default action for the dashboard.
		/// </summary>
		/// <returns>The result</returns>
		[HttpGet]
		[Route("")]
		public ActionResult Get() {
			return RedirectToAction("List", "PostMgr");
		}
    }
}