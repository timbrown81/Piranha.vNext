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
	/// Serializes image blocks.
	/// </summary>
	public class ImageSerializer : ISerializer
	{
		/// <summary>
		/// Deserializes the given JSON data.
		/// </summary>
		/// <param name="str">The JSON data</param>
		/// <returns>The deserialized object</returns>
		public object Deserialize(string str) {
			var block = new Blocks.Image() { 
				MediaId = !String.IsNullOrWhiteSpace(str) ? (Guid?)new Guid(str) : null
			};
			if (block.MediaId.HasValue) {
				using (var api = new Api()) {
					block.Media = api.Media.GetSingle(block.MediaId.Value);
					if (block.Media == null)
						block.MediaId = null;
				}
			}
			return block;
		}

		/// <summary>
		/// Serializes the given object to JSON.
		/// </summary>
		/// <param name="data">The model</param>
		/// <returns>The serialized object</returns>
		public string Serialize(object model) {
			return ((Blocks.Image)model).MediaId.ToString(); ;
		}
	}
}
