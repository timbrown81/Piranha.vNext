#if DEBUG
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
using System.Web.Routing;

namespace Piranha
{
	/// <summary>
	/// Main application class for the manager interface. Only for
	/// debugging purposes.
	/// </summary>
	public class MvcApplication : System.Web.HttpApplication
	{
		/// <summary>
		/// Starts the application.
		/// </summary>
		protected void Application_Start() {
			// Register routes & areas
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			AreaRegistration.RegisterAllAreas();

			// Initialize the application instance
			Piranha.App.Init(c => {
				c.Env = new AspNet.Env();
				c.Security = new AspNet.Security.SimpleSecurity("admin", "password");
				c.Store = new EntityFramework.Store();
			});
		}
	}
}
#endif