/*
 * Piranha CMS
 * Copyright (c) 2014, Håkan Edling, All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */

using System;
using System.Collections.Generic;

namespace Piranha.Manager
{
	/// <summary>
	/// Static class for defining manager config settings.
	/// </summary>
	public static class Config
	{
		#region Inner classes
		/// <summary>
		/// A block of config settings.
		/// </summary>
		public sealed class ConfigBlock
		{
			#region Properties
			/// <summary>
			/// Gets/sets the section name.
			/// </summary>
			public string Section { get; set; }

			/// <summary>
			/// Gets/sets the block name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets config rows.
			/// </summary>
			public IList<ConfigRow> Rows { get; set; }
			#endregion

			/// <summary>
			/// Default constructor.
			/// </summary>
			public ConfigBlock() {
				Rows = new List<ConfigRow>();
			}

			/// <summary>
			/// Creates a new block from the given data.
			/// </summary>
			/// <param name="section">The section name</param>
			/// <param name="name">The block name</param>
			/// <param name="rows">The available rows.</param>
			public ConfigBlock(string section, string name, IList<ConfigRow> rows) {
				Section = section;
				Name = name;
				Rows = rows;
			}

			/// <summary>
			/// Refreshes all of the config values from the data store.
			/// </summary>
			/// <param name="api">An open api</param>
			public void Refresh(Api api) {
				foreach (var row in Rows) {
					foreach (var col in row.Columns) {
						foreach (var item in col.Items) {
							item.Value = Utils.GetParam<string>(item.Param, s => s);
						}
					}
				}
			}
		}

		/// <summary>
		/// A row element in the config block.
		/// </summary>
		public sealed class ConfigRow
		{
			#region Properties
			/// <summary>
			/// Gets/sets the available columns.
			/// </summary>
			public IList<ConfigColumn> Columns { get; set; }
			#endregion

			/// <summary>
			/// Default constructor.
			/// </summary>
			public ConfigRow() {
				Columns = new List<ConfigColumn>();
			}

			/// <summary>
			/// Creates a config row from the given data.
			/// </summary>
			/// <param name="columns">The available columns</param>
			public ConfigRow(IList<ConfigColumn> columns) {
				Columns = columns;
			}
		}

		/// <summary>
		/// A column element in the config block.
		/// </summary>
		public sealed class ConfigColumn
		{
			#region Properties
			/// <summary>
			/// Gets/sets the available config items.
			/// </summary>
			public IList<ConfigItem> Items { get; set; }
			#endregion

			/// <summary>
			/// Default constructor.
			/// </summary>
			public ConfigColumn() {
				Items = new List<ConfigItem>();
			}

			/// <summary>
			/// Creates a column from the given data.
			/// </summary>
			/// <param name="items">The available config items</param>
			public ConfigColumn(IList<ConfigItem> items) {
				Items = items;
			}
		}

		/// <summary>
		/// Base class for all config items.
		/// </summary>
		public abstract class ConfigItem 
		{
			/// <summary>
			/// Gets/sets the display name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets the unique param name.
			/// </summary>
			public string Param { get; set; }

			/// <summary>
			/// Gets/sets the current param value.
			/// </summary>
			public string Value { get; set; }
		}

		/// <summary>
		/// Config item from strings.
		/// </summary>
		public sealed class ConfigString : ConfigItem { }

		/// <summary>
		/// Config item for multiline strings.
		/// </summary>
		public sealed class ConfigText : ConfigItem { }

		/// <summary>
		/// Config item for numbers.
		/// </summary>
		public sealed class ConfigInteger : ConfigItem { }
		#endregion

		/// <summary>
		/// The static collection of config blocks.
		/// </summary>
		public static readonly IList<ConfigBlock> Blocks = new List<ConfigBlock>();

		/// <summary>
		/// Refreshes the values of all config items.
		/// </summary>
		/// <param name="api"></param>
		public static void Refresh(Api api) {
			foreach (var block in Blocks) {
				block.Refresh(api);
			}
		}
	}
}
