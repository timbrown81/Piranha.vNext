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
using FluentValidation;

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
		/// Gets/sets the content id.
		/// </summary>
		public Guid ContentId { get; set; }

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
		/// Gets/sets the content.
		/// </summary>
		public Content Content { get; set; }
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
			// ensure to call the base class OnSave which will validate the model
			base.OnSave();

			if (Hooks.Models.Comment.OnSave != null)
				Hooks.Models.Comment.OnSave(this);

			// Handle possible notifications
			HandleNotifications();

			// Remove parent from model cache
			App.ModelCache.Remove<Models.Content>(this.ContentId);
		}

		/// <summary>
		/// Called before the model is deleted by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnDelete() {
			if (Hooks.Models.Comment.OnDelete != null)
				Hooks.Models.Comment.OnDelete(this);

			// Remove parent from model cache
			App.ModelCache.Remove<Models.Content>(this.ContentId);
		}
		#endregion

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate() {
			var validator = new CommentValidator();
			return validator.Validate(this);
		}

		#region Private methods
		/// <summary>
		/// Takes care of any mail notifications that should be sent.
		/// </summary>
		private void HandleNotifications() {
			if (Config.Comments.NotifyAuthor || Config.Comments.NotifyModerators) {
				if (App.Mail != null) {
					using (var api = new Api()) {
						var content = api.Content.GetSingle(ContentId);

						if (content != null) {
							var recipients = new List<Mail.Address>();
							var mail = new Mail.Message();

							if (Hooks.Mail.OnCommentMail != null) {
								// Generate custom mail
								mail = Hooks.Mail.OnCommentMail(content, this);
							} else {
								// Generate default mail
								var ui = new Client.Helpers.UIHelper();
								mail.Subject = "New comment posted on " + content.Title;
								mail.Body = String.Format(Mail.Defaults.NewComment,
									ui.GravatarUrl(Email, 80),
									App.Env.AbsoluteUrl(ui.Permalink(content)),
									content.Title,
									Author,
									Created.ToString("yyyy-MM-dd HH:mm:ss"),
									Body.Replace("\n", "<br/>"));
							}

							if (Config.Comments.NotifyAuthor && !String.IsNullOrWhiteSpace(content.Author.Email)) {
								// Add author address
								recipients.Add(new Mail.Address() {
									Email = content.Author.Email,
									Name = content.Author.Name
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
				} else {
					App.Logger.Log(Log.LogLevel.ERROR, "Models.Comment.HandleNotifications: No mail provider configured.");
				}
			}
		}
		#endregion

		#region Validator
		private class CommentValidator : AbstractValidator<Comment>
		{
			public CommentValidator() {
				RuleFor(m => m.UserId).Length(0, 128);
				RuleFor(m => m.Author).Length(0, 128);
				RuleFor(m => m.Email).Length(0, 128);
				RuleFor(m => m.UserId).Length(0, 16);
				RuleFor(m => m.UserAgent).Length(0, 128);
				RuleFor(m => m.WebSite).Length(0, 128);
				RuleFor(m => m.SessionID).Length(0, 64);
			}
		}
		#endregion
	}
}