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
using System.Dynamic;
using System.Linq;

namespace Piranha.Manager.Models.Config
{
	/// <summary>
	/// View model for the config edit view.
	/// </summary>
	public class EditModel
	{
		#region Inner classes
		/// <summary>
		/// The config params available for caching.
		/// </summary>
		public class CacheModel
		{
			public int Expires { get; set; }
			public int MaxAge { get; set; }
		}

		/// <summary>
		/// The config params available for comments.
		/// </summary>
		public class CommentModel
		{
			public bool ModerateAnonymous { get; set; }
			public bool ModerateAuthorized { get; set; }
			public bool NotifyAuthor { get; set; }
			public bool NotifyModerators { get; set; }
			public string Moderators { get; set; }
		}

		/// <summary>
		/// The config params available for the site.
		/// </summary>
		public class SiteModel
		{
			public string Title { get; set; }
			public string Description { get; set; }
			public int ArchivePageSize { get; set; }
		}

		/// <summary>
		/// Module specified params.
		/// </summary>
		public class ParamModel
		{
			/// <summary>
			/// Gets/sets the unique param name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets the param value.
			/// </summary>
			public string Value { get; set; }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the cache configuration.
		/// </summary>
		public CacheModel Cache { get; set; }

		/// <summary>
		/// Gets/sets the comment configuration.
		/// </summary>
		public CommentModel Comments { get; set; }

		/// <summary>
		/// Gets/sets the site configuration.
		/// </summary>
		public SiteModel Site { get; set; }

		/// <summary>
		/// Gets/sets the config params.
		/// </summary>
		public IList<object> Params { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public EditModel() {
			Cache = new CacheModel();
			Comments = new CommentModel();
			Site = new SiteModel();
			Params = new List<object>();
		}

		/// <summary>
		/// Gets the edit model with the current configuration values.
		/// </summary>
		/// <returns>The model</returns>
		public static EditModel Get(Api api) {
			var m = new EditModel();

			m.Cache.Expires = Piranha.Config.Cache.Expires;
			m.Cache.MaxAge = Piranha.Config.Cache.MaxAge;

			m.Comments.ModerateAnonymous = Piranha.Config.Comments.ModerateAnonymous;
			m.Comments.ModerateAuthorized = Piranha.Config.Comments.ModerateAuthorized;
			m.Comments.NotifyAuthor = Piranha.Config.Comments.NotifyAuthor;
			m.Comments.NotifyModerators = Piranha.Config.Comments.NotifyModerators;
			m.Comments.Moderators = Piranha.Config.Comments.Moderators;

			m.Site.Title = Piranha.Config.Site.Title;
			m.Site.Description = Piranha.Config.Site.Description;
			m.Site.ArchivePageSize = Piranha.Config.Site.ArchivePageSize;

			Manager.Config.Refresh(api);
			foreach (var block in Manager.Config.Blocks.OrderBy(b => b.Section).ThenBy(b => b.Name)) {
				foreach (var row in block.Rows) {
					foreach (var col in row.Columns) {
						foreach (var item in col.Items)
							m.Params.Add(new ParamModel() { 
								Name = item.Param, 
								Value = item.Value 
							});
					}
				}
			}
			return m;
		}
	}
}