/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using Newtonsoft.Json;
using System;
using System.Linq;

namespace Piranha.Models
{
	/// <summary>
	/// The content block holds the actual content.
	/// </summary>
	public sealed class ContentBlock : Data.IModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the row id.
		/// </summary>
		public Guid RowId { get; set; }

		/// <summary>
		/// Gets/sets the sort order.
		/// </summary>
		public int SortOrder { get; set; }

		/// <summary>
		/// Gets/sets the column size for the block.
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Gets/sets the runtime type of the serialized value.
		/// </summary>
		public string ClrType { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized value.
		/// </summary>
		public string Value { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the row this content block is a part of.
		/// </summary>
		public ContentRow Row { get; set; }
		#endregion

		/// <summary>
		/// Gets/sets the deserialized body.
		/// </summary>
		public Extend.IBlock Body {
			get {
				var type = App.Extensions.Blocks
					.Where(r => r.ValueType.FullName == ClrType)
					.Select(r => r.ValueType)
					.SingleOrDefault();
				if (type != null) {
					var serializer = App.Serializers[type];

					if (serializer != null)
						return (Extend.IBlock)serializer.Deserialize(Value);
					return (Extend.IBlock)JsonConvert.DeserializeObject(Value, type);
				} else {
					App.Logger.Log(Log.LogLevel.ERROR, "ContentBlock: Deserialization error. Couldn't find CLRType " + ClrType);
				}
				return null;
			}
			set {
				var serializer = App.Serializers[value.GetType()];

				ClrType = value.GetType().FullName;
				Value = serializer != null ? serializer.Serialize(value) : JsonConvert.SerializeObject(value);
			}
		}
	}
}
