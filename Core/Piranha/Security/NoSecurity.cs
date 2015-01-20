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

namespace Piranha.Security
{
	/// <summary>
	/// Dummy security provider
	/// </summary>
	public sealed class NoSecurity : ISecurity
	{
		/// <summary>
		/// Authenticates the given user credentials without
		/// signing in the user.
		/// </summary>
		/// <param name="username">The user</param>
		/// <param name="password">The password</param>
		/// <returns>If the credentials was authenticated successfully</returns>
		public bool Authenticate(string username, string password) {
			return true;
		}

		/// <summary>
		/// Signs in the user with the given username and password.
		/// </summary>
		/// <param name="username">The username</param>
		/// <param name="password">The password</param>
		/// <returns>If the user was signed in</returns>
		public bool SignIn(string username, string password) {
			return true;
		}

		/// <summary>
		/// Signs out the currently authenticated user.
		/// </summary>
		public void SignOut() { }

		/// <summary>
		/// Checks if the current user is authenticated.
		/// </summary>
		/// <returns>If the user is authenticated</returns>
		public bool IsAuthenticated() {
			return true;
		}

		/// <summary>
		/// Checks if the current user has the given role.
		/// </summary>
		/// <param name="role">The role</param>
		/// <returns>If the user has the role</returns>
		public bool IsInRole(string role) {
			return true;
		}

		/// <summary>
		/// Gets the user id of the currently authenticated user.
		/// </summary>
		/// <returns>The user id</returns>
		public string GetUserId() {
			return "";
		}
	}
}
