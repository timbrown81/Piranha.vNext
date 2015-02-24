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
	/// Interface for a redirect response.
	/// </summary>
	public interface IRedirectResponse : IResponse
	{
		/// <summary>
		/// The url that the request should be redirected to.
		/// </summary>
		string Url { get; set; }

		/// <summary>
		/// If the redirect should be permanent.
		/// </summary>
		bool IsPermanent { get; set; }
	}
}
