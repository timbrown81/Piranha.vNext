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
using System.Collections.Generic;

namespace Piranha.Models
{
	/// <summary>
	/// The row groups content blocks together.
	/// </summary>
	public sealed class ContentRow : Data.IModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the content id.
		/// </summary>
		public Guid ContentId { get; set; }

		/// <summary>
		/// Gets/sets the sort order.
		/// </summary>
		public int SortOrder { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the content this row is a part of.
		/// </summary>
		public Content Content { get; set; }

		/// <summary>
		/// Gets/sets the available content blocks for this row.
		/// </summary>
		public IList<ContentBlock> Blocks { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ContentRow() {
			Blocks = new List<ContentBlock>();
		}
	}
}
