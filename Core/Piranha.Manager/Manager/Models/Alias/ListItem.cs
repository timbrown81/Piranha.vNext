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

namespace Piranha.Manager.Models.Alias
{
	/// <summary>
	/// View model for the alias list.
	/// </summary>
	public class ListItem
	{
		#region Properties
		/// <summary>
		/// Gets/sets unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Get/sets the old url.
		/// </summary>
		public string OldUrl { get; set; }

		/// <summary>
		/// Gets/sets the new url.
		/// </summary>
		public string NewUrl { get; set; }

		/// <summary>
		/// Gets/sets when the model was created.
		/// </summary>
		public string Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public string Updated { get; set; }

		/// <summary>
		/// Gets/sets if this item was saved during the last operation.
		/// </summary>
		public bool Saved { get; set; }
		#endregion
	}
}