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
using System.Collections.Generic;

namespace Piranha.Models
{
	/// <summary>
	/// Comments are used for discussing posts.
	/// </summary>
	public sealed class Comment : Model, Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the post id.
		/// </summary>
		public Guid PostId { get; set; }

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
		/// Gets/sets the optional IP adress from where the comment was made.
		/// </summary>
		public string IP { get; set; }

		/// <summary>
		/// Gets/sets the user agent.
		/// </summary>
		public string UserAgent { get; set; }

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
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the post.
		/// </summary>
		public Post Post { get; set; }
		#endregion

		#region Internal events
		/// <summary>
		/// Called when the model is materialized by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnLoad() {
			if (Hooks.Models.Comment.OnLoad != null)
				Hooks.Models.Comment.OnLoad(this);
		}

		/// <summary>
		/// Called before the model is saved by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnSave() {
			if (Hooks.Models.Comment.OnSave != null)
				Hooks.Models.Comment.OnSave(this);

			// Handle possible notifications
			HandleNotifications();

			// Remove parent post from model cache
			App.ModelCache.Remove<Models.Post>(this.PostId);
		}

		/// <summary>
		/// Called before the model is deleted by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnDelete() {
			if (Hooks.Models.Comment.OnDelete != null)
				Hooks.Models.Comment.OnDelete(this);

			// Remove parent post from model cache
			App.ModelCache.Remove<Models.Post>(this.PostId);
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Takes care of any mail notifications that should be sent.
		/// </summary>
		private void HandleNotifications() {
			if (Config.Comments.NotifyAuthor || Config.Comments.NotifyModerators) {
				using (var api = new Api()) {
					var post = api.Posts.GetSingle(PostId);

					if (post != null) {
						var recipients = new List<Mail.Address>();
						var mail = new Mail.Message();

						if (Hooks.Mail.OnCommentMail != null) { 
							mail = Hooks.Mail.OnCommentMail(post, this);
						} else {
							var ui = new Client.Helpers.UIHelper();

							// Default formatting
							mail.Subject = "New comment posted on " + post.Title;
							mail.Body =
								"<html>" +
								"<head>" +
								"  <style type='text/css'>" +
								"    body { background: #eee; color: #444; padding: 20px; }" +
								"    h1 { font-size: 24px; }" +
								"    p { background: #fff; padding: 20px; border-radius: 3px; }" +
								"  </style>" +
								"</head>" +
								"<body>" +
								"  <div class='comment'>" +
								"    <img src='" + ui.GravatarUrl(Email, 80) + "' />" +
								"    <h1>New comment on " + post.Title + " </h1>" +
								"    <p>" + Body +
								"    </p>" +
								"  </div>" +
								"</body>" +
								"</html>";
						}

						if (Config.Comments.NotifyAuthor && !String.IsNullOrWhiteSpace(post.Author.Email)) {
							// Add author address
							recipients.Add(new Mail.Address() {
								Email = post.Author.Email,
								Name = post.Author.Name
							});
						}

						if (Config.Comments.NotifyModerators && !String.IsNullOrWhiteSpace(Config.Comments.Moderators)) {
							// Add moderator addresses
							foreach (var moderator in Config.Comments.Moderators.Split(new char[] { ',' })) {
								recipients.Add(new Mail.Address() {
									Email = moderator.Trim(),
									Name = moderator.Trim()
								});
							}
						}

						// Send mail
						App.Mail.Send(mail, recipients.ToArray());
					}
				}
			}
		}
		#endregion
	}
}