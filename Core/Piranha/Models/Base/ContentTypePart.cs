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

namespace Piranha.Models.Base
{
	/// <summary>
	/// Base class for defining content type parts.
	/// </summary>
	public abstract class ContentTypePart : Model, Data.IModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the id of the content type.
		/// </summary>
		public Guid TypeId { get; set; }

		/// <summary>
		/// Get/sets the internal id.
		/// </summary>
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the CLR type of the part value.
		/// </summary>
		public string CLRType { get; set; }

		/// <summary>
		/// Gets/sets if this part should support multiple values.
		/// </summary>
		public bool IsCollection { get; set; }

		/// <summary>
		/// Gets/sets the current sort order.
		/// </summary>
		public int Order { get; set; }
		#endregion
	}
}
