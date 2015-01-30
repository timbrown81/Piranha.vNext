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
	/// Main site configuration.
	/// </summary>
	public static class Site
	{
		/// <summary>
		/// Gets/sets the site title.
		/// </summary>
		public static string Title {
			get { return Utils.GetParam<string>("site_title", s => s); }
			set { Utils.SetParam("site_title", value); }
		}

		/// <summary>
		/// Gets/sets the short site tagline.
		/// </summary>
		public static string Tagline {
			get { return Utils.GetParam<string>("site_tagline", s => s); }
			set { Utils.SetParam("site_tagline", value); }
		}

		/// <summary>
		/// Gets/sets the site description.
		/// </summary>
		public static string Description {
			get { return Utils.GetParam<string>("site_description", s => s); }
			set { Utils.SetParam("site_description", value); }
		}

		/// <summary>
		/// Gets/sets the global last modification date.
		/// </summary>
		public static DateTime LastModified {
			get { return Utils.GetParam<DateTime>("site_lastmodified", s => DateTime.Parse(s)); }
			set { Utils.SetParam("site_lastmodified", value.ToString("yyyy-MM-dd HH:mm:ss")); }
		}

		/// <summary>
		/// Gets/sets the number of posts that should be displayed on
		/// an archive page.
		/// </summary>
		public static int ArchivePageSize {
			get { return Utils.GetParam<int>("archive_pagesize", s => Convert.ToInt32(s)); }
			set { Utils.SetParam("archive_pagesize", value); }
		}

		/// <summary>
		/// Gets/sets the main archive title.
		/// </summary>
		public static string ArchiveTitle {
			get { return Utils.GetParam<string>("archive_title", s => s); }
			set { Utils.SetParam("archive_title", value); }
		}

		/// <summary>
		/// Gets/sets the main archive meta keywords.
		/// </summary>
		public static string ArchiveKeywords {
			get { return Utils.GetParam<string>("archive_keywords", s => s); }
			set { Utils.SetParam("archive_keywords", value); }
		}

		/// <summary>
		/// Gets/sets the main archive meta description.
		/// </summary>
		public static string ArchiveDescription {
			get { return Utils.GetParam<string>("archive_description", s => s); }
			set { Utils.SetParam("archive_description", value); }
		}
	}
}
