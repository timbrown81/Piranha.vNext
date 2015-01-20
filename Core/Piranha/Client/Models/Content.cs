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

namespace Piranha.Client.Models
{
	/// <summary>
	/// Class for defining the current content being viewed.
	/// </summary>
	public sealed class Content
	{
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the content title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the meta keyworkds.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets the virtual path to the document
		/// </summary>
		public string VirtualPath { get; set; }

		/// <summary>
		/// Gets/sets the current content type.
		/// </summary>
		public ContentType Type { get; set; }
	}
}
