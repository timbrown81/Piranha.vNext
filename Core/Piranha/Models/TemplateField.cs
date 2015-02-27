/*
 * Copyright (c) 2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;

namespace Piranha.Models
{
	/// <summary>
	/// Template fields are used for defining custom fields
	/// available for templates.
	/// </summary>
	public sealed class TemplateField : Data.IModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the template id.
		/// </summary>
		public Guid TemplateId { get; set; }

		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets the sort order of the field.
		/// </summary>
		public int SortOrder { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the CLR type of the field value.
		/// </summary>
		public string CLRType { get; set; }

		/// <summary>
		/// Gets/sets if this field has multiple values.
		/// </summary>
		public bool IsCollection { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the template.
		/// </summary>
		public Template Template { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TemplateField() {
			SortOrder = 1;
		}
	}
}
