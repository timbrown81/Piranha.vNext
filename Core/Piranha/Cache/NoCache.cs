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
	/// Dummy cache provider.
	/// </summary>
	public class NoCache : ICache
	{
		/// <summary>
		/// Gets the cached model for the given id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public T Get<T>(string id) {
			return default(T);
		}

		/// <summary>
		/// Sets the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="obj">The model</param>
		public void Set(string id, object obj) { }

		/// <summary>
		/// Removes the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		public void Remove(string id) { }
	}
}