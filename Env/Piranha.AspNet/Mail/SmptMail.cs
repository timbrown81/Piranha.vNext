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
using System.Net.Mail;
using Piranha.Mail;

namespace Piranha.AspNet.Mail
{
	/// <summary>
	/// Standard SMTP mail provider implementation.
	/// </summary>
	public class SmptMail : Piranha.Mail.IMail
	{
		/// <summary>
		/// Sends the given mail message to the list of recipients.
		/// </summary>
		/// <param name="message">The message</param>
		/// <param name="recipients">The recipients</param>
		/// <returns>If the action was successful</returns>
		public void Send(Message message, params Address[] recipients) {
			using (var mail = new MailMessage()) { 
				// Create mail message
				mail.Subject = message.Subject;
				mail.Body = message.Body;
				mail.IsBodyHtml = true;

				// Add recipients
				foreach (var r in recipients) {
					mail.To.Add(new MailAddress(r.Email, r.Name));
				}

				// Send mail
				var smtp = new SmtpClient();
				smtp.Send(mail);
			}
		}
	}
}
