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

namespace Piranha.Server
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
						// Handle startpage
						if (request.Segments.Length == 0 && App.Handlers.Start != null)
							response = App.Handlers.Start.Handle(api, request);

						// Handle alias redirects
						if (response == null && App.Handlers.Aliases != null)
							response = App.Handlers.Aliases.Handle(api, request);

						// Handle request by keyword
						if (response == null && request.Segments.Length > 0) {
							var handler = App.Handlers[request.Segments[0]];
							if (handler != null)
								response = handler.Handle(api, request);
						}

						// Handle content
						if (response == null && App.Handlers.Content != null)
							response = App.Handlers.Content.Handle(api, request);

						// Handle archives
						if (response == null && App.Handlers.Archives != null)
							response = App.Handlers.Archives.Handle(api, request);
					}

					// Execute the response
					if (response != null)
						response.Execute();
				}
			}
		}
	}
}
