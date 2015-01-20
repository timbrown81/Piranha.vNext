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

namespace Piranha.Extend.Builder
{
	/// <summary>
	/// Content parts are used to build up content.
	/// </summary>
	internal sealed class ContentPart
	{
		#region Properties
		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets if this part accepts multiple values.
		/// </summary>
		public bool IsCollection { get; set; }

		/// <summary>
		/// Gets/sets the CLR type of the value(s).
		/// </summary>
		public string CLRType { get; set; }
		#endregion
	}
}
