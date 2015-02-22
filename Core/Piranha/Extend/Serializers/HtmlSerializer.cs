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

namespace Piranha.Extend.Serializers
{
	/// <summary>
	/// Serializes html components.
	/// </summary>
	public class HtmlSerializer : ISerializer
	{
		/// <summary>
		/// Deserializes the given JSON data.
		/// </summary>
		/// <param name="str">The JSON data</param>
		/// <returns>The deserialized object</returns>
		public object Deserialize(string str) {
			return new Components.Html() { 
				Value = str
			};
		}

		/// <summary>
		/// Serializes the given object to JSON.
		/// </summary>
		/// <param name="data">The model</param>
		/// <returns>The serialized object</returns>
		public string Serialize(object model) {
			return ((Components.Html)model).Value;
		}
	}
}
