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
	/// Multi line string extension.
	/// </summary>
	[Component(Name="Text", Type=ComponentType.ContentBlock|ComponentType.TemplateField)]
	public class Text : Component<string>, IComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Text() : base(v => v) { }
	}
}
