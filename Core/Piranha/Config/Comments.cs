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

namespace Piranha.Config
{
	/// <summary>
	/// Commenting configuration.
	/// </summary>
	public static class Comments
	{
		/// <summary>
		/// Gets/sets if comments from authorized users should be moderated.
		/// </summary>
		public static bool ModerateAuthorized {
			get { return Utils.GetParam<bool>("comment_moderate_authorized", s => Convert.ToBoolean(s)); }
			set { Utils.SetParam("comment_moderate_authorized", value); }
		}

		/// <summary>
		/// Gets/sets if comments from anonymous users should be moderated.
		/// </summary>
		public static bool ModerateAnonymous {
			get { return Utils.GetParam<bool>("comment_moderate_anonymous", s => Convert.ToBoolean(s)); }
			set { Utils.SetParam("comment_moderate_anonymous", value); }
		}

		/// <summary>
		/// Gets/sets if the author should be notified by email when a comment is posted.
		/// </summary>
		public static bool NotifyAuthor {
			get { return Utils.GetParam<bool>("comment_notify_author", s => Convert.ToBoolean(s)); }
			set { Utils.SetParam("comment_notify_author", value); }
		}

		/// <summary>
		/// Gets/sets if moderators should be notified by email when a comment is posted.
		/// </summary>
		public static bool NotifyModerators {
			get { return Utils.GetParam<bool>("comment_notify_moderators", s => Convert.ToBoolean(s)); }
			set { Utils.SetParam("comment_notify_moderators", value); }
		}

		/// <summary>
		/// Gets/sets a comma separated list of email adresses.
		/// </summary>
		public static string Moderators {
			get { return Utils.GetParam<string>("comment_moderators", s => s); }
			set { Utils.SetParam("comment_moderators", value); }
		}
	}
}
