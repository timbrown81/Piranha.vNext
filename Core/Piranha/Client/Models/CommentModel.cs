/*
 * Piranha CMS
 * Copyright (c) 2014, Håkan Edling, All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
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
