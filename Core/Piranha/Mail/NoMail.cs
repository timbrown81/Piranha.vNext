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

namespace Piranha.Mail
{
	/// <summary>
	/// Dummy mail provider that disables mail.
	/// </summary>
	public class NoMail : IMail
	{
		/// <summary>
		/// Sends the given mail message to the list of recipients.
		/// </summary>
		/// <param name="message">The message</param>
		/// <param name="recipients">The recipients</param>
		/// <returns>If the action was successful</returns>
		public void Send(Message message, params Address[] recipients) { }
	}
}
