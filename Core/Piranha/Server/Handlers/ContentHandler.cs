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
using System.Text;
using Piranha.Client.Models;
using Piranha.Models;

namespace Piranha.Server.Handlers
{
	public class ContentHandler : IHandler
	{
		/// <summary>
		/// Handles the request and rewrites it to the appropriate route.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="request">The current request</param>
		/// <returns>The response</returns>
		public IResponse Handle(Api api, IRequest request) {
			if (request.Segments.Length > 0) {
				ContentType? type = null;
				var offset = 0;

				// 1: Try to find content type by slug
				if (request.Segments[0] == Config.Permalinks.PageSlug) {
					type = ContentType.Page;
				} else if (request.Segments[0] == Config.Permalinks.PostSlug) { 
					type = ContentType.Post;
				}

				// 2: Try to find content type with empty slug
				if (type.HasValue) {
					offset = 1;
				} else {
					if (String.IsNullOrWhiteSpace(Config.Permalinks.PageSlug))
						type = ContentType.Page;
					else if (String.IsNullOrWhiteSpace(Config.Permalinks.PostSlug))
						type= ContentType.Post;
				}

				// 3: Try to find the requested content
				if (type.HasValue && request.Segments.Length > offset) {
					var content = ContentModel.GetBySlug(type.Value, request.Segments[offset]);

					if (content != null) {
						var route = content.Route;
						var ui = new Client.Helpers.UIHelper();

						// Append extra url segments
						for (var n = offset + 1; n < request.Segments.Length; n++) {
							route += "/" + request.Segments[n];
						}
		
						// Set current
						App.Env.SetCurrent(new Current() {
							Id = content.Id,
							Title = content.Title,
							Keywords = content.MetaKeywords,
							Description = content.MetaDescription,
							VirtualPath = GetVirtualPath(content, offset > 0 ? request.Segments[0] : ""),
							Type = GetCurrentType(content)
						});

						var response = request.RewriteResponse();

						response.Route = route;
						response.Params = request.Params.Concat(new Param[] { 
							new Param() { Key = "id", Value = content.Id.ToString() }
						}).ToArray();

						return response;
					}
				}
			}
			return null;
		}

		#region Private methods
		/// <summary>
		/// Gets the current type for the given content.
		/// </summary>
		/// <param name="content">The content model</param>
		/// <returns>The type</returns>
		private CurrentType GetCurrentType(ContentModel content) {
			if (content.Type == ContentType.Page)
				return CurrentType.Page;
			return CurrentType.Post;
		}

		/// <summary>
		/// Gets the virtual path for requested content.
		/// </summary>
		/// <param name="content">The content</param>
		/// <param name="slug">The content type slug</param>
		/// <returns>The virtual path</returns>
		private string GetVirtualPath(ContentModel content, string slug) {
			var sb = new StringBuilder("~/");

			if (!String.IsNullOrWhiteSpace(slug)) {
				sb.Append(slug).Append("/");
			}
			return sb.Append(content.Slug).ToString();
		}
		#endregion
	}
}