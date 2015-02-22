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
	/// Html extension.
	/// </summary>
	[Component(Name="Html", Type=ComponentType.ContentBlock|ComponentType.TemplateField)]
	public class Html : Component<string>, IComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Html() : base(v => v) { }
	}
}
