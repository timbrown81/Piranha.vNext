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
	/// <summary>
	/// Handler for serving the applications startapge.
	/// </summary>
	public class StartHandler : IHandler
	{
		/// <summary>
		/// Tries to handle an incoming request.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="request">The incoming route request</param>
		/// <returns>The result</returns>
		public IResponse Handle(Api api, IRequest request) {
			//
			// TODO:
			//
			// 1: Check navigation structure for startpage
			// 2: Check for archive page for content group with empty slug
			// 3: Check for archive page if only a single content group exists
			//
			var start = api.Content.Get(where: c => c.Type == Models.ContentType.Page).FirstOrDefault();

			if (start != null) {
				var content = ContentModel.GetById(start.Id);

				// Set current
				App.Env.SetCurrent(new Current() {
					Id = content.Id,
					Title = content.Title,
					Keywords = content.MetaKeywords,
					Description = content.MetaDescription,
					VirtualPath = "~/",
					Type = CurrentType.Start
				});

				var response = request.RewriteResponse();

				response.Route = content.Route;
				response.Params = request.Params.Concat(new Param[] { 
					new Param() { Key = "id", Value = content.Id.ToString() }
				}).ToArray();

				return response;
			}
			return null;
		}
	}
}
