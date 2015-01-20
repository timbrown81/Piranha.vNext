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

namespace Piranha.Client.Models
{
	/// <summary>
	/// Application comment model.
	/// </summary>
	public class CommentModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the optional user id.
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Gets/sets the author name.
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// Gets/sets the author email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the optional author website.
		/// </summary>
		public string WebSite { get; set; }

		/// <summary>
		/// Gets/sets the comment body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Gets/sets the optional Session ID that made the comment.
		/// </summary>
		public string SessionID { get; set; }

		/// <summary>
		/// Gets/sets if the comment is approved or not.
		/// </summary>
		public bool IsApproved { get; set; }

		/// <summary>
		/// Gets/sets if the comment has been marked as spam.
		/// </summary>
		public bool IsSpam { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets the available ratings.
		/// </summary>
		public RatingsModel Ratings { get; set; }
		#endregion
	}
}
