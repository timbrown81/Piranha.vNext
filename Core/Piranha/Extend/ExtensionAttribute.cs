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

namespace Piranha.Extend
{
	/// <summary>
	/// Attribute for marking a class as an extension that should be
	/// imported by the extension manager.
	/// </summary>
	public sealed class ExtensionAttribute : Attribute
	{
		#region Properties
		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the extension type.
		/// </summary>
		public ExtensionType Type { get; set; }
		#endregion
	}
}
