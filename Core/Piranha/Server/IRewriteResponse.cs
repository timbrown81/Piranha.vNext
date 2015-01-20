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
	/// Interface for a rewrite response.
	/// </summary>
	public interface IRewriteResponse : IResponse
	{
		/// <summary>
		/// The internal route that the request should be 
		/// rewritten to.
		/// </summary>
		string Route { get; set; }

		/// <summary>
		/// Optional query params.
		/// </summary>
		Param[] Params { get; set; }
	}
}
