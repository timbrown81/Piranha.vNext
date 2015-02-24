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
using System.Linq;

namespace Piranha.Manager.Models.PostType
{
	/// <summary>
	/// View model for the post type list.
	/// </summary>
	public class ListModel
	{
		#region Inner classes
		/// <summary>
		/// An item in the post type list.
		/// </summary>
		public class PostTypeListItem
		{
			/// <summary>
			/// Gets/sets the unique id.
			/// </summary>
			public Guid Id { get; set; }

			/// <summary>
			/// Gets/sets the name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets the date the post type was initially created.
			/// </summary>
			public DateTime Created { get; set; }

			/// <summary>
			/// Gets/sets the date the post type was initially updated.
			/// </summary>
			public DateTime Updated { get; set; }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the available items.
		/// </summary>
		public IEnumerable<PostTypeListItem> Items { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ListModel() {
			Items = new List<PostTypeListItem>();
		}

		/// <summary>
		/// Gets the post type list model.
		/// </summary>
		/// <returns>The model</returns>
		public static ListModel Get() {
			using (var api = new Api()) {
				var m = new ListModel();

				m.Items = api.PostTypes.Get().Select(t => new PostTypeListItem() {
					Id = t.Id,
					Name = t.Name,
					Created = t.Created,
					Updated = t.Updated
				});
				return m;
			}
		}
	}
}