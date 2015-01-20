/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System.Web.Mvc;

namespace Piranha.Areas.Manager
{
	public class ManagerAreaRegistration : AreaRegistration
	{
		public override string AreaName {
			get { return "Manager"; }
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"Manager_default",
				"manager/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}