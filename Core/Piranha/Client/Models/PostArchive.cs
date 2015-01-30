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
using Piranha.Models;

namespace Piranha.Client.Models
{
	/// <summary>
	/// The default post archive.
	/// </summary>
	public class PostArchive : IArchived
	{
		#region Members
		/// <summary>
		/// The singleton instance of the default post archive.
		/// </summary>
		public static PostArchive Instance = new PostArchive();
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the archive title.
		/// </summary>
		public string ArchiveTitle {
			get { return Config.Site.ArchiveTitle; }
		}

		/// <summary>
		/// Gets the archive slug.
		/// </summary>
		public string ArchiveSlug {
			get { return Config.Permalinks.PostArchiveSlug; }
		}

		/// <summary>
		/// Gets/sets the optional archive meta keywords.
		/// </summary>
		public string MetaKeywords {
			get { return Config.Site.ArchiveKeywords; }
		}

		/// <summary>
		/// Gets/sets the optional archive meta description.
		/// </summary>
		public string MetaDescription {
			get { return Config.Site.ArchiveDescription; }
		}

		/// <summary>
		/// Gets/sets the optional archive view.
		/// </summary>
		public string ArchiveView {
			get { return ""; }
		}
		#endregion

		/// <summary>
		/// Default private constructor.
		/// </summary>
		private PostArchive() { }
	}
}
