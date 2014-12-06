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
using Piranha.Models;
using Piranha.Mail;

namespace Piranha.Hooks
{
	/// <summary>
	/// The mail hooks available.
	/// </summary>
	public static class Mail
	{
		/// <summary>
		/// The delegates used by the mail hooks.
		/// </summary>
		public static class Delegates
		{
			/// <summary>
			/// Delegate for formatting a comment mail.
			/// </summary>
			/// <param name="post">The current post</param>
			/// <param name="comment">The new comment</param>
			/// <returns>The formatted mail message</returns>
			public delegate Message CommentMailDelegate(Post post, Comment comment);
		}

		/// <summary>
		/// Called when the comment notification mail is to be formatted.
		/// </summary>
		public static Delegates.CommentMailDelegate OnCommentMail;
	}
}
