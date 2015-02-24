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

namespace Piranha.Mail
{
	/// <summary>
	/// A mail address that can be used as sender or recipient.
	/// </summary>
	public sealed class Address
	{
		#region Properties
		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the email address.
		/// </summary>
		public string Email { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Address() { }

		/// <summary>
		/// Creates a new mail address with the given fields.
		/// </summary>
		/// <param name="name">The display name</param>
		/// <param name="email">The email address</param>
		public Address(string name, string email) {
			Name = name;
			Email = email;
		}
	}
}
