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

namespace Piranha.Config
{
	/// <summary>
	/// Server caching configuration.
	/// </summary>
	public static class Cache
	{
		/// <summary>
		/// Gets/sets the expiration time in minutes for the public http cache.
		/// </summary>
		public static int Expires {
			get { return Utils.GetParam<int>("cache_expires", s => Convert.ToInt32(s)); }
			set { Utils.SetParam("cache_expires", value); }
		}

		/// <summary>
		/// Gets/sets the max age in minutes for the public http cache.
		/// </summary>
		public static int MaxAge {
			get { return Utils.GetParam<int>("cache_maxage", s => Convert.ToInt32(s)); }
			set { Utils.SetParam("cache_maxage", value); }					
		}
	}
}
