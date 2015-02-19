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
using System.Collections.Generic;
using System.Linq;

namespace Piranha.Client.Models
{
	/// <summary>
	/// The sitemap is used to represent the current site structure
	/// of published pages.
	/// </summary>
	public class SiteMap
	{
		#region Inner classes
		/// <summary>
		/// An item in the sitemap.
		/// </summary>
		public class SiteMapItem
		{
			#region Properties
			/// <summary>
			/// Gets/sets the id of the related content.
			/// </summary>
			public Guid ContentId { get; set; }

			/// <summary>
			/// Gets/sets the title.
			/// </summary>
			public string Title { get; set; }

			/// <summary>
			/// Gets/sets the unique slug.
			/// </summary>
			public string Slug { get; set; }

			/// <summary>
			/// Gets/sets if the page should be hidden from navigations.
			/// </summary>
			public bool IsHidden { get; set; }

			/// <summary>
			/// Gets/sets the level of the item.
			/// </summary>
			public int Level { get; set; }

			/// <summary>
			/// Gets/sets the child items.
			/// </summary>
			public IEnumerable<SiteMapItem> Items { get; set; }
			#endregion

			/// <summary>
			/// Default constructor.
			/// </summary>
			public SiteMapItem() {
				Items = new List<SiteMapItem>();
			}

			/// <summary>
			/// Checks if the item and it's children contains the given id.
			/// </summary>
			/// <param name="contentId">The unique conent id</param>
			/// <returns>If the id is contained within the item</returns>
			internal bool Contains(Guid contentId) {
				if (ContentId == contentId)
					return true;
				foreach (var item in Items) {
					if (item.Contains(contentId))
						return true;
				}
				return false;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the collection of sitemap items.
		/// </summary>
		public IEnumerable<SiteMapItem> Items { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SiteMap() {
			Items = new List<SiteMapItem>();
		}

		/// <summary>
		/// Gets the current sitemap structure.
		/// </summary>
		/// <returns>The sitemap</returns>
		public static SiteMap Get() {
			var sitemap = App.ModelCache.GetSiteMap();

			if (sitemap == null) {
				sitemap = Utils.GetParam<SiteMap>("sitemap", s => JsonConvert.DeserializeObject<SiteMap>(s));

				if (sitemap == null)
					sitemap = new SiteMap();

				App.ModelCache.SetSiteMap(sitemap);
			}
			return sitemap;
		}

		/// <summary>
		/// Gets the level in the hierarchy with the specified parent.
		/// </summary>
		/// <param name="id">The parent id</param>
		/// <param name="level">The requested level</param>
		/// <returns>The level</returns>
		public IEnumerable<SiteMapItem> GetLevel(Guid? id, int level) {
			return GetLevel(Items, id, level);
		}

		#region Private methods
		/// <summary>
		/// Gets the level in the hierarchy with the specified parent.
		/// </summary>
		/// <param name="items">The items to search</param>
		/// <param name="id">The parent id</param>
		/// <param name="level">The requested level</param>
		/// <returns>The level</returns>
		private IEnumerable<SiteMapItem> GetLevel(IEnumerable<SiteMapItem> items, Guid? id, int level) {
			if (items == null || items.Count() == 0 || items.First().Level == level)
				return items;
			if (id.HasValue) {
				foreach (var item in items) {
					if (item.Contains(id.Value))
						return GetLevel(item.Items, id, level);
				}
			}
			return null;
		}
		#endregion
	}
}
