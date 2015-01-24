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
using Piranha.Client.Models;

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
				var content = ContentModel.GetBySlug(slug, Models.ContentType.Post);

				if (content != null) {
					var route = content.Route;

					// Append extra url segments
					for (var n = 2; n < request.Segments.Length; n++) {
						route += "/" + request.Segments[n];
					}

					// Set current
					App.Env.SetCurrent(new Client.Models.Current() {
						Id = content.Id,
						Title = content.Title,
						Keywords = content.MetaKeywords,
						Description = content.MetaDescription,
						VirtualPath = "~/content/" + route,
						Type = Client.Models.CurrentType.Post
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
