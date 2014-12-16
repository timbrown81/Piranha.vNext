/*
 * Copyright (c) 2014 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Piranha.Mail;

namespace Piranha.AspNet.Mail
{
	/// <summary>
	/// Standard SMTP mail provider implementation.
	/// </summary>
	public class SmtpMail : Piranha.Mail.IMail
	{
		#region Internal classes
		/// <summary>
		/// Internal class for messages to be send using the
		/// smtp mail provider.
		/// </summary>
		internal class SmtpMessage
		{
			/// <summary>
			/// Gets/sets the message data.
			/// </summary>
			public Message Data { get; set; }

			/// <summary>
			/// Gets/sets the recipients.
			/// </summary>
			public Address[] Recipients { get; set; }
		}
		#endregion

		#region Members
		/// <summary>
		/// The private message queue.
		/// </summary>
		private readonly Queue<SmtpMessage> messages = new Queue<SmtpMessage>();
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SmtpMail() {
			// Potentially bad as this thread is not monitored by
			// ASP.NET and might be terminated on pool recycling
			// by the IIS. Evaluate to upgrade to .NET 4.5.2 and
			// use QueueBackgroundWorkItem. However if this thread
			// is terminated it's not the end of the world, and
			// the queue will be emptied quite quickly
			Task.Factory.StartNew(() => {
				while (true) {
					while (messages.Count > 0) {
						var message = messages.Dequeue();

						using (var mail = new MailMessage()) {
							try {
								// Create mail message
								mail.Subject = message.Data.Subject;
								mail.Body = message.Data.Body;
								mail.IsBodyHtml = true;

								// Add recipients
								foreach (var r in message.Recipients) {
									mail.To.Add(new MailAddress(r.Email, r.Name));
								}

								// Send mail
								var smtp = new SmtpClient();
								smtp.Send(mail);
							} catch (Exception ex) {
								// Log error
								App.Logger.Log(Log.LogLevel.ERROR, "AspNet.Mail.SmtpMail: Error sending mail", ex);
							}
						}
					}
					Thread.Sleep(3000);
				}
			});
		}

		/// <summary>
		/// Sends the given mail message to the list of recipients.
		/// </summary>
		/// <param name="message">The message</param>
		/// <param name="recipients">The recipients</param>
		/// <returns>If the action was successful</returns>
		public void Send(Message message, params Address[] recipients) {
			// Add the message to the internal queue
			messages.Enqueue(new SmtpMessage() {
				Data = message,
				Recipients = recipients
			});
		}
	}
}
