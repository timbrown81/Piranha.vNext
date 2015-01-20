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

namespace Piranha.Mail
{
	/// <summary>
	/// Class for representing a mail message.
	/// </summary>
	public sealed class Message
	{
		#region Properties
		/// <summary>
		/// Gets/sets the mail subject.
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Gets/sets the body of the mail message. 
		/// </summary>
		public string Body { get; set; }
		#endregion
	}
}
