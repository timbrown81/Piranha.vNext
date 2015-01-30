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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Piranha.Client.Helpers
{
	/// <summary>
	/// Helper class for UI methods.
	/// </summary>
	public class UIHelper
	{
		#region Members
		private const string META_TAG = "<meta name=\"{0}\" content=\"{1}\">\n";
		#endregion

		#region Properties
		/// <summary>
		/// Gets if the current request is for a page.
		/// </summary>
		public bool IsPage {
			get {
				var current = App.Env.GetCurrent();
				return current.Type == Models.CurrentType.Page || current.Type == Models.CurrentType.Start;
			}
		}

		/// <summary>
		/// Gets if the current request is for a post.
		/// </summary>
		public bool IsPost {
			get {
				return App.Env.GetCurrent().Type == Models.CurrentType.Post;
			}
		}

		/// <summary>
		/// Gets if the current request is for the startpage.
		/// </summary>
		public bool IsStart {
			get {
				return App.Env.GetCurrent().Type == Models.CurrentType.Start;
			}
		}
		#endregion

		/// <summary>
		/// Gets the block with the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The block content</returns>
		public string Block(string slug) {
			using (var api = new Api()) {
				var block = api.Blocks.GetSingle(slug);

				if (block != null && block.Body != null)
					return block.Body;
				return "";
			}
		}

		/// <summary>
		/// Renders the head meta data from the currently requested content.
		/// </summary>
		/// <returns>The meta data</returns>
		public string Head() {
			var sb = new StringBuilder();
			var current = App.Env.GetCurrent();

			if (current != null) {
				// Generator
				sb.Append(String.Format(META_TAG, "generator", "Piranha CMS " + Utils.GetFileVersion()));

				// Meta tags
				if (!String.IsNullOrWhiteSpace(current.Keywords))
					sb.Append(String.Format(META_TAG, "keywords", current.Keywords));
				if (!String.IsNullOrWhiteSpace(current.Description))
					sb.Append(String.Format(META_TAG, "description", current.Description));

				// Open graph
				if (!String.IsNullOrWhiteSpace(current.Title))
					sb.Append(String.Format(META_TAG, "og:title", current.Title));
				if (!String.IsNullOrWhiteSpace(current.Description))
					sb.Append(String.Format(META_TAG, "og:description", current.Description));
				if (current.Type == Models.CurrentType.Start)
					sb.Append(String.Format(META_TAG, "og:type", "website"));
				else sb.Append(String.Format(META_TAG, "og:type", "article"));
				if (!String.IsNullOrWhiteSpace(current.VirtualPath))
					sb.Append(String.Format(META_TAG, "og:url", App.Env.AbsoluteUrl(current.VirtualPath)));

				if (Hooks.UI.Head.Render != null)
					Hooks.UI.Head.Render(sb);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Renders the archive permalink for the given category.
		/// </summary>
		/// <param name="category">The category</param>
		/// <returns>The generated permalink</returns>
		public string Permalink(Piranha.Models.Category category) {
			var sb = new StringBuilder("~/");

			return App.Env.Url(sb.Append(Config.Permalinks.CategoryArchiveSlug).Append("/").Append(category.Slug).ToString());
		}

		/// <summary>
		/// Renders the permalink for the given content.
		/// </summary>
		/// <param name="content">The content model</param>
		/// <returns>The generated permalink</returns>
		public string Permalink(Models.ContentModel content) {
			var sb = new StringBuilder("~/");

			if (content.Type == Piranha.Models.ContentType.Page)
				sb.Append(Config.Permalinks.PageSlug);
			else sb.Append(Config.Permalinks.PostSlug);

			return App.Env.Url(sb.Append("/").Append(content.Slug).ToString());
		}

		/// <summary>
		/// Renders the permalink for the given content.
		/// </summary>
		/// <param name="content">The content model</param>
		/// <returns>The generated permalink</returns>
		public string Permalink(Piranha.Models.Content content) {
			var sb = new StringBuilder("~/");

			if (content.Type == Piranha.Models.ContentType.Page)
				sb.Append(Config.Permalinks.PageSlug);
			else sb.Append(Config.Permalinks.PostSlug);

			return App.Env.Url(sb.Append("/").Append(content.Slug).ToString());
		}

		/// <summary>
		/// Generates a gravatar url for the given email and size.
		/// </summary>
		/// <param name="email">The email address</param>
		/// <param name="size">The size in pixels</param>
		/// <returns>The gravatar url</returns>
		public string GravatarUrl(string email, int size) {
			var md5 = new MD5CryptoServiceProvider();

			var encoder = new UTF8Encoding();
			var hash = new MD5CryptoServiceProvider();
			var bytes = hash.ComputeHash(encoder.GetBytes(email));

			var sb = new StringBuilder(bytes.Length * 2);
			for (int n = 0; n < bytes.Length; n++) {
				sb.Append(bytes[n].ToString("X2"));
			}
			return "http://www.gravatar.com/avatar/" + sb.ToString().ToLower() + (size > 0 ? "?s=" + size : "");
		}

		/// <summary>
		/// Renders the url to the given media file.
		/// </summary>
		/// <param name="media">The media file</param>
		/// <param name="width">Optional width</param>
		/// <param name="height">Optional height</param>
		/// <returns>The url</returns>
		public string Media(Piranha.Models.Media media, int? width, int? height) {
			return App.Env.Url("~/media.ashx/" + media.Slug);
		}

		/// <summary>
		/// Renders the thumbnail url to the given media file.
		/// </summary>
		/// <param name="media">The media file</param>
		/// <returns>The url</returns>
		public string Thumbnail(Piranha.Models.Media media, int? size) {
			return App.Env.Url("~/media.ashx/" + Utils.FormatMediaSlug(media.Slug, size, size));
		}

		/// <summary>
		/// Return the site structure as an ul/li list with the current page selected.
		/// </summary>
		/// <param name="start">The start level of the menu</param>
		/// <param name="stop">The stop level of the menu</param>
		/// <param name="levels">The number of levels. Use this if you don't know the start level</param>
		/// <param name="root">Optional rootnode for the menu to start from</param>
		/// <param name="css">Optional css class for the outermost container</param>
		/// <returns>A rendered menu</returns>
		public string Menu(int start = 1, int stop = Int32.MaxValue, int levels = 0, string css = "menu") {
			StringBuilder str = new StringBuilder();
			IEnumerable<Models.SiteMap.SiteMapItem> sm = null;
			var content = App.Env.GetCurrent();
			var current = content != null && (content.Type == Models.CurrentType.Page || content.Type == Models.CurrentType.Start) ? (Guid?)content.Id : null;

			if (current.HasValue || start == 1) {
				sm = Models.SiteMap.Get().GetLevel(current, start);

				if (sm != null) {
					if (stop == Int32.MaxValue && levels > 0 && sm.Count() > 0)
						stop = sm.First().Level + Math.Max(0, levels - 1);
					RenderUL(current, sm, str, stop, css);
				}
			}
			return str.ToString();
		}

		/// <summary>
		/// Renders an UL list for the given sitemap elements
		/// </summary>
		/// <param name="curr">The current page</param>
		/// <param name="items">The sitemap items</param>
		/// <param name="sb">The string builder</param>
		/// <param name="stop">The desired stop level</param>
		private void RenderUL(Guid? curr, IEnumerable<Models.SiteMap.SiteMapItem> items, StringBuilder sb, int stop, string css = "") {
			if (items != null && items.Count() > 0 && items.First().Level <= stop) {
				// Render level start
				if (Hooks.UI.Menu.RenderLevelStart != null) {
					Hooks.UI.Menu.RenderLevelStart(sb, css);
				} else {
					sb.AppendLine("<ul class=\"" + css + "\">");
				}
				// Render items
				foreach (var item in items)
					if (!item.IsHidden) RenderLI(curr, item, sb, stop);
				// Render level end
				if (Hooks.UI.Menu.RenderLevelEnd != null) {
					Hooks.UI.Menu.RenderLevelEnd(sb, css);
				} else {
					sb.AppendLine("</ul>");
				}
			}
		}

		/// <summary>
		/// Renders an LI element for the given sitemap node.
		/// </summary>
		/// <param name="curr">The current page</param>
		/// <param name="item">The sitemap element</param>
		/// <param name="str">The string builder</param>
		/// <param name="stop">The desired stop level</param>
		private void RenderLI(Guid? curr, Models.SiteMap.SiteMapItem item, StringBuilder sb, int stop) {
			//if (page.GroupId == Guid.Empty || HttpContext.Current.User.IsMember(page.GroupId)) {
			var active = curr.HasValue && curr.Value == item.ContentId;
			var childactive = curr.HasValue && item.Contains(curr.Value);

			// Render item start
			if (Hooks.UI.Menu.RenderItemStart != null) {
				Hooks.UI.Menu.RenderItemStart(sb, item, active, childactive);
			} else {
				var hasChild = item.Items.Count() > 0 ? " has-child" : "";
				sb.AppendLine("<li" + (active ? " class=\"active" + hasChild + "\"" :
					(childactive ? " class=\"active-child" + hasChild + "\"" :
					(item.Items.Count() > 0 ? " class=\"has-child\"" : ""))) + ">");
			}
			// Render item link
			sb.AppendLine(String.Format("<a href=\"{0}\">{1}</a>", App.Env.Url("~/" + item.Slug), item.Title));

			// Render subpages
			if (item.Items.Count() > 0)
				RenderUL(curr, item.Items, sb, stop);
			// Render item end
			if (Hooks.UI.Menu.RenderItemEnd != null) {
				Hooks.UI.Menu.RenderItemEnd(sb, item, active, childactive);
			} else {
				sb.AppendLine("</li>");
			}
			//}
		}
	}
}
