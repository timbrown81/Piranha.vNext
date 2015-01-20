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

namespace Piranha.Cache
{
	/// <summary>
	/// Interface for creating an application cache provider.
	/// </summary>
	public interface ICache
	{
		/// <summary>
		/// Gets the cached model for the given id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		T Get<T>(string id);

		/// <summary>
		/// Sets the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="obj">The model</param>
		void Set(string id, object obj);

		/// <summary>
		/// Removes the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		void Remove(string id);
	}
}
