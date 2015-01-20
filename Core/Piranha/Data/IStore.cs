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
	/// Interface defining the different methods that should be provided
	/// by a data store.
	/// </summary>
	public interface IStore
	{
		/// <summary>
		/// Opens a new session on the current store.
		/// </summary>
		/// <returns>The new session</returns>
		ISession OpenSession();
	}
}
