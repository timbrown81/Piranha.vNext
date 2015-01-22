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

namespace Piranha.Extend.Blocks
{
	/// <summary>
	/// A single image from the media-library.
	/// </summary>
	[Block(Name="Single image")]
	public class Image : IBlock
	{
		/// <summary>
		/// Gets/sets the id of the selected media.
		/// </summary>
		public Guid? MediaId { get; set; }

		/// <summary>
		/// Gets/sets the selected media.
		/// </summary>
		public Models.Media Media { get; set; }
	}
}
