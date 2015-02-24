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
	/// Base interface for all responses that can be returned
	/// to a Piranha CMS request.
	/// </summary>
	public interface IResponse
	{
		/// <summary>
		/// Executes the response.
		/// </summary>
		void Execute();
	}
}
