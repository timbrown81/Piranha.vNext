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

namespace Piranha.Manager
{
	/// <summary>
	/// Static class for defining the manager menu.
	/// </summary>
	public static class Menu
	{
		#region Inner classes
		/// <summary>
		/// An item in the manager menu.
		/// </summary>
		public class MenuItem
		{
			#region Properties
			/// <summary>
			/// Gets/sets the internal id.
			/// </summary>
			public string InternalId { get; set; }

			/// <summary>
			/// Gets/sets the display name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets the optional css class.
			/// </summary>
			public string Css { get; set; }

			/// <summary>
			/// Gets/sets the manager controller.
			/// </summary>
			public string Controller { get; set; }

			/// <summary>
			/// Gets/sets the default action to invoke.
			/// </summary>
			public string Action { get; set; }

			/// <summary>
			/// Gets/sets the available items.
			/// </summary>
			public IList<MenuItem> Items { get; set; }
			#endregion

			/// <summary>
			/// Default constructor.
			/// </summary>
			public MenuItem() {
				Items = new List<MenuItem>();
			}
		}
		#endregion

		/// <summary>
		/// The basic manager menu.
		/// </summary>
		public static IList<MenuItem> Items = new List<MenuItem>() { 
			new MenuItem() {
				InternalId = "Content", Name = "Content", Css = "glyphicon glyphicon-pencil", Items = new List<MenuItem>() {
					new MenuItem() {
						InternalId = "Blocks", Name = "Blocks", Controller = "BlockMgr", Action = "List"
					},
					new MenuItem() {
						InternalId = "Categories", Name = "Categories", Controller = "CategoryMgr", Action = "List"
					},
					new MenuItem() {
						InternalId = "Posts", Name = "Posts", Controller = "PostMgr", Action = "List"
					}
				}
			},
			new MenuItem() {
				InternalId = "Settings", Name = "Settings", Css = "glyphicon glyphicon-wrench", Items = new List<MenuItem>() {
					new MenuItem() {
						InternalId = "Aliases", Name = "Aliases", Controller = "AliasMgr", Action = "List"
					},
					new MenuItem() {
						InternalId = "Authors", Name = "Authors", Controller = "AuthorMgr", Action = "List"
					},
					new MenuItem() {
						InternalId = "PageTypes", Name = "Page types", Controller = "PageTypeMgr", Action = "List"
					},
					new MenuItem() {
						InternalId = "PostTypes", Name = "Post types", Controller = "PostTypeMgr", Action = "List"
					}
				}
			},
			new MenuItem() {
				InternalId = "System", Name = "System", Css = "glyphicon glyphicon-cog", Items = new List<MenuItem>() {
					new MenuItem() {
						InternalId = "Config", Name = "Config", Controller = "ConfigMgr", Action = "List"
					}
				}
			}
		};
	}
}