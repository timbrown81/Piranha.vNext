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

namespace Piranha.Models
{
	/// <summary>
	/// Interface for archived models. The information is used to
	/// build meta-data for a generated archive page.
	/// </summary>
	public interface IArchived
	{
		/// <summary>
		/// Gets/sets the archive title.
		/// </summary>
		string ArchiveTitle { get; }

		/// <summary>
		/// Gets the archive slug.
		/// </summary>
		string ArchiveSlug { get; }

		/// <summary>
		/// Gets/sets the optional archive meta keywords.
		/// </summary>
		string MetaKeywords { get; }

		/// <summary>
		/// Gets/sets the optional archive meta description.
		/// </summary>
		string MetaDescription { get; }

		/// <summary>
		/// Gets/sets the optional archive view.
		/// </summary>
		string ArchiveView { get; }
	}
}
