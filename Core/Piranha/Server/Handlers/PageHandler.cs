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
	/// <summary>
	/// Handler for routing requests for pages.
	/// </summary>
	public class PageHandler : IHandler
	{
		/// <summary>
		/// Tries to handle an incoming request.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="request">The incoming route request</param>
		/// <returns>The result</returns>
		public IResponse Handle(Api api, IRequest request) {
			var slug = request.Segments.Length > 0 ? request.Segments[0] : "";
			var now = DateTime.Now;
			var route = "";

			slug = slug != "" ? slug : 
				api.Pages.GetSingle(where: p => !p.ParentId.HasValue && p.SortOrder == 1 && p.Published <= now).Slug;

			// Get startpage or by slug.
			var page = Client.Models.PageModel.GetBySlug(slug);

			if (page != null) {
				route = !String.IsNullOrWhiteSpace(page.Route) ? page.Route : "page";

				// Append extra url segments
				for (var n = 1; n < request.Segments.Length; n++) {
					route += "/" + request.Segments[n];
				}

				// Set current
				App.Env.SetCurrent(new Client.Models.Content() {
					Id = page.Id,
					Title = page.Title,
					Keywords = page.Keywords,
					Description = page.Description,
					VirtualPath = "~/" + page.Slug,
					Type = !page.ParentId.HasValue && page.SortOrder == 1 ? Client.Models.ContentType.Start : Client.Models.ContentType.Page
				});

				var response = request.RewriteResponse();

				response.Route = route;
				response.Params = request.Params.Concat(new Param[] { 
					new Param() { Key = "id", Value = page.Id.ToString() }
				}).ToArray();

				return response;
			}
			return null;
		}
	}
}
