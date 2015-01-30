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
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class BlockAttribute : Attribute
	{
		/// <summary>
		/// Gets/sets the display name of the block type.
		/// </summary>
		public string Name { get; set; }
	}
}
