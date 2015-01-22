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
using Piranha.Extend;

namespace Piranha.Server.Routing
{
	/// <summary>
	/// The route module is responsible for parsing the incoming request
	/// and routing it to the correct part of Piranha CMS.
	/// </summary>
	public class RouteModule : IModule
	{
		/// <summary>
		/// Initializes the module. This method should be used for
		/// ensuring runtime resources and registering hooks.
		/// </summary>
		public void Init() {
			// Add the default handlers
			App.Logger.Log(Log.LogLevel.INFO, "RouteModule.Init: Registering default handlers");
			App.Handlers.Aliases = new Handlers.AliasHandler();
			App.Handlers.Pages = new Handlers.PageHandler();
			App.Handlers.Posts = new Handlers.PostHandler();
			App.Handlers.Add("content", new Handlers.ContentHandler());
			App.Handlers.Add("media.ashx", new Handlers.MediaHandler());

			// Attach the router
			App.Logger.Log(Log.LogLevel.INFO, "RouteModule.Init: Attaching OnBeginRequest");
			Hooks.App.Request.OnBeginRequest += Router.OnBeginRequest;
		}
	}
}
