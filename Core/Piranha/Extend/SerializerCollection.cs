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

namespace Piranha.Extend
{
	/// <summary>
	/// The serializers.
	/// </summary>
	public sealed class SerializerCollection
	{
		#region Members
		/// <summary>
		/// The private collection of serializers.
		/// </summary>
		private readonly Dictionary<Type, ISerializer> serializers;
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the serializer for the given type.
		/// </summary>
		/// <param name="type">The serializer type</param>
		/// <returns>The registered serializer, null if not found</returns>
		public ISerializer this[Type type] {
			get {
				ISerializer handler;

				if (serializers.TryGetValue(type, out handler))
					return handler;
				return null;
			}
			set { serializers[type] = value; }
		}
		#endregion

		/// <summary>
		/// Default internal constructor.
		/// </summary>
		internal SerializerCollection() {
			serializers = new Dictionary<Type, ISerializer>();
		}

		/// <summary>
		/// Adds a new or replaces the registered serializer for 
		/// the given type.
		/// </summary>
		/// <param name="type">The handler type</param>
		/// <param name="handler">The request handler</param>
		public void Add(Type type, ISerializer handler) {
			serializers[type] = handler;
		}

		/// <summary>
		/// Removes the serializer for the given type.
		/// </summary>
		/// <param name="type">The handler type</param>
		public void Remove(Type type) {
			serializers.Remove(type);
		}
	}
}
