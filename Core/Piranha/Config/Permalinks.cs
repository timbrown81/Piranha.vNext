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
	/// Permalink configuration.
	/// </summary>
	public static class Permalinks
	{
		/// <summary>
		/// Gets/sets the page slug prefix.
		/// </summary>
		public static string PageSlug {
			get { return Utils.GetParam<string>("permalink_page", s => s); }
			set { Utils.SetParam("permalink_page", value); }
		}

		/// <summary>
		/// Gets/sets the post slug prefix.
		/// </summary>
		public static string PostSlug {
			get { return Utils.GetParam<string>("permalink_post", s => s); }
			set { Utils.SetParam("permalink_post", value); }
		}

		/// <summary>
		/// Gets/sets the post archive slug prefix.
		/// </summary>
		public static string PostArchiveSlug {
			get { return Utils.GetParam<string>("permalink_postarchive", s => s); }
			set { Utils.SetParam("permalink_postarchive", value); }
		}

		/// <summary>
		/// Gets/sets the category archive slug prefix.
		/// </summary>
		public static string CategoryArchiveSlug {
			get { return Utils.GetParam<string>("permalink_categoryarchive", s => s); }
			set { Utils.SetParam("permalink_categoryarchive", value); }
		}

		/// <summary>
		/// Gets/sets the tag archive slug prefix.
		/// </summary>
		public static string TagArchiveSlug {
			get { return Utils.GetParam<string>("permalink_tagarchive", s => s); }
			set { Utils.SetParam("permalink_tagarchive", value); }
		}	
	}
}
