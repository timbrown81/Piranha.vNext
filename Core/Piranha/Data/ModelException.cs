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
using System.Linq;

namespace Piranha.Data
{
	/// <summary>
	/// Model validation exception.
	/// </summary>
	public sealed class ModelException : Exception
	{
		#region Members
		/// <summary>
		/// Gets the array of model errors.
		/// </summary>
		public readonly ModelError[] Errors;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="message">The error message</param>
		/// <param name="errors">The model errors</param>
		public ModelException(string message, IEnumerable<ModelError> errors) {
			Errors = errors.ToArray();
		}
	}
}
