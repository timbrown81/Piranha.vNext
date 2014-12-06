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
