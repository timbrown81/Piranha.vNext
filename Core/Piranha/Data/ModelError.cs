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

namespace Piranha.Data
{
	/// <summary>
	/// Class for representing model validatation errors.
	/// </summary>
	public sealed class ModelError
	{
		#region Inner classes
		/// <summary>
		/// The different types of validation errors.
		/// </summary>
		public enum ErrorType
		{
			Duplicate,
			Required,
			StringLength
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the model member. 
		/// </summary>
		public string Member { get; private set; }

		/// <summary>
		/// Gets the validation error type.
		/// </summary>
		public ErrorType Type { get; private set; }

		/// <summary>
		/// Gets the error message.
		/// </summary>
		public string Message { get; private set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="member">The model member</param>
		/// <param name="type">The error type</param>
		/// <param name="message">The error message</param>
		internal ModelError(string member, ErrorType type, string message) {
			Member = member;
			Type = type;
			Message = message;
		}
	}
}
