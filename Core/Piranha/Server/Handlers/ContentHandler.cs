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
using System.Linq;

namespace Piranha.Server.Handlers
{
	public class ContentHandler : IHandler
	{
		/// <summary>
		/// Tries to handle an incoming request.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="request">The incoming route request</param>
		/// <returns>The result</returns>
		public IResponse Handle(Api api, IRequest request) {
			var slug = request.Segments.Length > 1 ? request.Segments[1] : "";

			if (!String.IsNullOrWhiteSpace(slug)) {
				var content = api.Content.GetSingle(where: c => c.Slug == slug && c.Published <= DateTime.Now);

				if (content != null) {
					var route = content.Route;

					// Append extra url segments
					for (var n = 2; n < request.Segments.Length; n++) {
						route += "/" + request.Segments[n];
					}

					// Set current
					App.Env.SetCurrent(new Client.Models.Content() {
						Id = content.Id,
						Title = content.Title,
						Keywords = content.MetaKeywords,
						Description = content.MetaDescription,
						VirtualPath = "~/content/" + content.Template.Route,
						Type = Client.Models.ContentType.Post
					});

					var response = request.RewriteResponse();

					response.Route = route;
					response.Params = request.Params.Concat(new Param[] { 
						new Param() { Key = "id", Value = content.Id.ToString() }
					}).ToArray();

					return response;
				}
			}
			return null;
		}
	}
}
