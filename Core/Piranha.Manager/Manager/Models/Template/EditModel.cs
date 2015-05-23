/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Piranha.Models;

namespace Piranha.Manager.Models.Template
{
	/// <summary>
	/// View model for the template edit view.
	/// </summary>
	public sealed class EditModel
	{
		#region Inner classes
		public sealed class Field
		{
			/// <summary>
			/// Gets/sets the unique id.
			/// </summary>
			public Guid? Id { get; set; }

			/// <summary>
			/// Gets/sets the internal id.
			/// </summary>
			[Required, StringLength(32)]
			public string InternalId { get; set; }

			/// <summary>
			/// Gets/sets the display name.
			/// </summary>
			[Required, StringLength(128)]
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets the CLR type of the field value.
			/// </summary>
			[Required, StringLength(512)]
			public string CLRType { get; set; }

			/// <summary>
			/// Gets/sets if this field has multiple values.
			/// </summary>
			public bool IsCollection { get; set; }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public ContentType Type { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		[Required, StringLength(128)]
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the optional route.
		/// </summary>
		[StringLength(255)]
		public string Route { get; set; }

		/// <summary>
		/// Gets/sets the optional view.
		/// </summary>
		[StringLength(255)]
		public string View { get; set; }

		/// <summary>
		/// Gets/sets the available fields.
		/// </summary>
		public IList<Field> Fields { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public EditModel() {
			Fields = new List<Field>();
		}

		/// <summary>
		/// Gets the edit model for the template with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public static EditModel GetById(Api api, Guid id) {
			var template = api.Templates.GetSingle(id);

			if (template != null)
				return Mapper.Map<Piranha.Models.Template, EditModel>(template);
			return null;
		}
	}
}