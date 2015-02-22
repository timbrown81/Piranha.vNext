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
	/// Serializes image components.
	/// </summary>
	public class ImageSerializer : ISerializer
	{
		/// <summary>
		/// Deserializes the given JSON data.
		/// </summary>
		/// <param name="str">The JSON data</param>
		/// <returns>The deserialized object</returns>
		public object Deserialize(string str) {
			var image = new Components.Image() { 
				Value = !String.IsNullOrWhiteSpace(str) ? (Guid?)new Guid(str) : null
			};
			if (image.Value.HasValue) {
				using (var api = new Api()) {
					image.Media = api.Media.GetSingle(image.Value.Value);
					if (image.Media == null)
						image.Value = null;
				}
			}
			return image;
		}

		/// <summary>
		/// Serializes the given object to JSON.
		/// </summary>
		/// <param name="data">The model</param>
		/// <returns>The serialized object</returns>
		public string Serialize(object model) {
			return ((Components.Image)model).Value.ToString(); ;
		}
	}
}
