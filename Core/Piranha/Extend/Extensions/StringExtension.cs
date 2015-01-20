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
	/// Single line string extension.
	/// </summary>
	[Extension(Name="String", Type=ExtensionType.Property|ExtensionType.Region)]
	public class StringExtension : SimpleExtension<string>, IExtension
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public StringExtension() : base(v => v) { }
	}
}
