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

namespace Piranha.Manager.Models.Post
{
	/// <summary>
	/// Models for changing a comments state.
	/// </summary>
	public class CommentState
	{
		#region Properties
		/// <summary>
		/// Gets/sets the post id.
		/// </summary>
		public Guid PostId { get; set; }

		/// <summary>
		/// Gets/sets the comment id.
		/// </summary>
		public Guid CommentId { get; set; }

		/// <summary>
		/// Gets/sets the status.
		/// </summary>
		public bool Status { get; set; }
		#endregion
	}
}