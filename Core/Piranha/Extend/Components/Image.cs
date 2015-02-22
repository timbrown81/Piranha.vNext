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

namespace Piranha.Extend.Components
{
	/// <summary>
	/// A single image from the media-library.
	/// </summary>
	[Component(Name="Image", Type=ComponentType.ContentBlock)]
	public class Image :  Component<Guid?>, IComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Image() : base(v => v) { }

		/// <summary>
		/// Gets/sets the selected media.
		/// </summary>
		public Models.Media Media { get; set; }
	}
}
