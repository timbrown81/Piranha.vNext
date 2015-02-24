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

namespace Piranha.Extend.Extensions
{
	/// <summary>
	/// Html extension.
	/// </summary>
	[Extension(Name="Html", Type=ExtensionType.Region)]
	public class HtmlExtension : SimpleExtension<string>, IExtension
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public HtmlExtension() : base(v => v) { }
	}
}
