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

namespace Piranha.Server.Routing
{
	/// <summary>
	/// Main router that handles the incoming requests.
	/// </summary>
	public static class Router
	{
		/// <summary>
		/// Executed on the beginning of each request.
		/// </summary>
		/// <param name="request">The current request</param>
		public static void OnBeginRequest(IRequest request) {
			if (!App.RoutingDisabled) {
				IResponse response = null;

				if (!request.RawUrl.StartsWith("/__browserLink/")) {
					using (var api = new Api()) {
						if (request.Segments.Length == 0) {
							// Handle startpage
							if (App.Handlers.Pages != null)
								response = App.Handlers.Pages.Handle(api, request);
						} else {
							// Handle alias redirects
							if (App.Handlers.Aliases != null)
								response = App.Handlers.Aliases.Handle(api, request);

							// Handle request by keyword
							if (response == null) {
								var handler = App.Handlers[request.Segments[0]];
								if (handler != null)
									response = handler.Handle(api, request);
							}

							// Handle posts
							if (response == null && App.Handlers.Posts != null)
								response = App.Handlers.Posts.Handle(api, request);

							// Handle pages
							if (response == null && App.Handlers.Pages != null)
								response = App.Handlers.Pages.Handle(api, request);
						}
					}

					// Execute the response
					if (response != null)
						response.Execute();
				}
			}
		}
	}
}
