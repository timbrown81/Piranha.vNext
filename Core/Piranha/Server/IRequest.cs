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
	/// Interface for defining a request to Piranha CMS.
	/// </summary>
	public interface IRequest
	{
		#region Properties
		/// <summary>
		/// Gets the full raw url requested.
		/// </summary>
		string RawUrl { get; }

		/// <summary>
		/// Gets the absolute path of the request url.
		/// </summary>
		string Path { get; }

		/// <summary>
		/// Gets the optional query of the requested url.
		/// </summary>
		string Query { get; }

		/// <summary>
		/// Gets the segments of the absolute path.
		/// </summary>
		string[] Segments { get; }

		/// <summary>
		/// Gets the params of the optional query.
		/// </summary>
		Param[] Params { get; }
		#endregion

		/// <summary>
		/// Creates a new redirect response for the current request.
		/// </summary>
		/// <returns>The response</returns>
		IRedirectResponse RedirectResponse();

		/// <summary>
		/// Creates a new rewrite response for the current request.
		/// </summary>
		/// <returns>The response</returns>
		IRewriteResponse RewriteResponse();

		/// <summary>
		/// Creates a new stream response for the current request.
		/// </summary>
		/// <returns>The response</returns>
		IStreamResponse StreamResponse();
	}
}
