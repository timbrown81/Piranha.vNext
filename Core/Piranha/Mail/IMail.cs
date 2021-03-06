﻿/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;

namespace Piranha.Mail
{
	/// <summary>
	/// Interface for creating a mail sender.
	/// </summary>
	public interface IMail
	{
		/// <summary>
		/// Sends the given mail message to the list of recipients.
		/// </summary>
		/// <param name="message">The message</param>
		/// <param name="recipients">The recipients</param>
		/// <returns>If the action was successful</returns>
		void Send(Message message, params Address[] recipients);
	}
}
