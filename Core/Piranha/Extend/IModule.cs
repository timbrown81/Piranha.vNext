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
	/// Interface for a module.
	/// </summary>
	public interface IModule
	{
		/// <summary>
		/// Initializes the module. This method should be used for
		/// ensuring runtime resources and registering hooks.
		/// </summary>
		void Init();
	}
}
