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
using System.Text;

namespace Piranha.Hooks
{
	/// <summary>
	/// The UI helper hooks available.
	/// </summary>
	public static class UI
	{
		/// <summary>
		/// The different delegates used.
		/// </summary>
		public static class Delegates 
		{
			/// <summary>
			/// Delegate for rendering content.
			/// </summary>
			/// <param name="sb">The string builder</param>
			public delegate void RenderDelegate(StringBuilder sb);

			/// <summary>
			/// Delegate for rendering a menu level.
			/// </summary>
			/// <param name="sb">The string builder</param>
			/// <param name="css">The optional css class</param>
			public delegate void RenderMenuLevelDelegate(StringBuilder sb, string css);

			/// <summary>
			/// Delegate for rendering a menu item.
			/// </summary>
			/// <param name="sb">The string builder</param>
			/// <param name="item">The current menu item</param>
			/// <param name="active">If the item is active</param>
			/// <param name="childactive">If the item has an active child node</param>
			public delegate void RenderMenuItemDelegate(StringBuilder sb, Client.Models.SiteMap.SiteMapItem item, bool active, bool childactive);
		}

		/// <summary>
		/// The hooks available for the @UI.Head helper.
		/// </summary>
		public static class Head
		{
			/// <summary>
			/// Called when head wants to render.
			/// </summary>
			public static Delegates.RenderDelegate Render;
		}

		/// <summary>
		/// The hooks available for the @UI.Menu helper.
		/// </summary>
		public static class Menu 
		{
			/// <summary>
			/// Called when the menu level wants to render its start tag.
			/// </summary>
			public static Delegates.RenderMenuLevelDelegate RenderLevelStart;

			/// <summary>
			/// Called when the menu level wants to render its end tag.
			/// </summary>
			public static Delegates.RenderMenuLevelDelegate RenderLevelEnd;

			/// <summary>
			/// Called when the menu item wants to render its start tag.
			/// </summary>
			public static Delegates.RenderMenuItemDelegate RenderItemStart;

			/// <summary>
			/// Called when the menu level wants to render its end tag.
			/// </summary>
			public static Delegates.RenderMenuItemDelegate RenderItemEnd;
		}
	}
}
