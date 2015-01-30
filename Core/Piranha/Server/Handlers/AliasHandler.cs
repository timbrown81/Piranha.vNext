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

namespace Piranha.Server.Handlers
{
	/// <summary>
	/// Handler for routing requests for aliases.
	/// </summary>
	public class AliasHandler : IHandler
	{
		/// <summary>
		/// Tries to handle an incoming request.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="request">The incoming route request</param>
		/// <returns>The result</returns>
		public IResponse Handle(Api api, IRequest request) {
			// First try to get the alias for the complete url
			var alias = api.Aliases.GetSingle(request.RawUrl);

			if (alias == null) {
				if (request.Params.Length > 0) {
					// Try to get alias for the url without query
					alias = api.Aliases.GetSingle(request.Path);

					if (alias != null) {
						var response = request.RedirectResponse();

						response.IsPermanent = alias.IsPermanent;
						response.Url = alias.NewUrl + request.Query;

						return response;
					}
				}
			} else {
				var response = request.RedirectResponse();

				response.IsPermanent = alias.IsPermanent;
				response.Url = alias.NewUrl;

				return response;
			}
			return null;
		}
	}
}
