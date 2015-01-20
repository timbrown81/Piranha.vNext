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

namespace Piranha.Extend
{
	/// <summary>
	/// Interface for all objects that should be treated as extensions.
	/// </summary>
	public interface IExtension
	{
		/// <summary>
		/// Transforms the extensions value for the client models.
		/// </summary>
		/// <returns>The transformed value</returns>
		object GetValue();
	}
}
