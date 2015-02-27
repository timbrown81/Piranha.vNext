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
	/// Templates are used define how content should be rendered.
	/// </summary>
	public sealed class Template : Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public ContentType Type { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the optional route.
		/// </summary>
		public string Route { get; set; }

		/// <summary>
		/// Gets/sets the optional view.
		/// </summary>
		public string View { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the available fields.
		/// </summary>
		public Data.StateList<TemplateField> Fields { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Template() {
			Fields = new Data.StateList<TemplateField>();
		}
	}
}
