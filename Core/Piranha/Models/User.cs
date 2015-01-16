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

using Microsoft.AspNet.Identity;
using System;

namespace Piranha.Models
{
	/// <summary>
	/// The main user entity used for authentication, both for the
	/// manager interface and client applications.
	/// </summary>
	public sealed class User : Data.IModel, IUser<Guid>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the optional author id.
		/// </summary>
		public Guid? AuthorId { get; set; }

		/// <summary>
		/// Gets/sets the username
		/// </summary>
		public string UserName { get; set; }
		#endregion
	}
}
