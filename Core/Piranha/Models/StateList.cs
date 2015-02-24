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

namespace Piranha.Models
{
	/// <summary>
	/// Generic list that keeps track of the items that have
	/// been removed. This is used by the entity framework
	/// provider to automatically delete removed entities.
	/// </summary>
	/// <typeparam name="T">The item type</typeparam>
	public class StateList<T> : List<T>
	{
		#region Members
		/// <summary>
		/// The private list of removed items.
		/// </summary>
		private IList<T> removed = new List<T>();
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public StateList() : base() { }

		/// <summary>
		/// Creates a new list with the specified capacity.
		/// </summary>
		/// <param name="capacity">The capacity</param>
		public StateList(int capacity) : base(capacity) { }

		/// <summary>
		/// Creates a new list from the given collection of items.
		/// </summary>
		/// <param name="collection">The items</param>
		public StateList(IEnumerable<T> collection) : base(collection) { }

		/// <summary>
		/// Removes the given item from the list.
		/// </summary>
		/// <param name="item">The item</param>
		/// <returns>If the item was successfully removed</returns>
		public new bool Remove(T item) {
			var ret = base.Remove(item);

			if (ret)
				removed.Add(item);
			return ret;
		}

		/// <summary>
		/// Gets the removed items.
		/// </summary>
		/// <returns>The items</returns>
		public IList<T> GetRemoved() {
			return removed;
		}
	}
}

/// <summary>
/// Extensions for the state list.
/// </summary>
public static class StateListExtensions
{
	/// <summary>
	/// Creates a state list from the current collection.
	/// </summary>
	/// <typeparam name="T">The item type</typeparam>
	/// <param name="collection">The item collection</param>
	/// <returns></returns>
	public static Piranha.Models.StateList<T> ToStateList<T>(this IEnumerable<T> collection) {
		return new Piranha.Models.StateList<T>(collection);
	}
}
