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
	/// A query param for a request.
	/// </summary>
	public sealed class Param
	{
		/// <summary>
		/// Gets/sets the key.
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Gets/sets the value.
		/// </summary>
		public string Value { get; set; }
	}
}
