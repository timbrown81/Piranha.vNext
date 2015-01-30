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
			/// <param name="content">The current post</param>
			/// <param name="comment">The new comment</param>
			/// <returns>The formatted mail message</returns>
			public delegate Message CommentMailDelegate(Content post, Comment comment);
		}

		/// <summary>
		/// Called when the comment notification mail is to be formatted.
		/// </summary>
		public static Delegates.CommentMailDelegate OnCommentMail;
	}
}
