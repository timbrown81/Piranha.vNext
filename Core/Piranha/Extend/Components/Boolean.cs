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
	/// Boolean extension.
	/// </summary>
	[Component(Name="Boolean", Type=ComponentType.TemplateField)]
	public class Boolean : Component<bool>, IComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Boolean() : base(v => v) { }
	}
}
