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

namespace Piranha.Manager.Models.Author
{
	/// <summary>
	/// View model for the author list.
	/// </summary>
	public class ListItem
	{
		#region Properties
		/// <summary>
		/// Gets/sets unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Get/sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the gravatar url.
		/// </summary>
		public string GravatarUrl { get; set; }

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