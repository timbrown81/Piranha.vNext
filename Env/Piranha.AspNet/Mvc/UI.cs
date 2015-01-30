/*
 * Copyright (c) 2014 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;
using System.Web;
using Piranha.Client.Helpers;
using Piranha.Client.Models;

namespace Piranha.AspNet.Mvc
{
	/// <summary>
	/// Static UI helper for ASP.NET MVC.
	/// </summary>
	public static class UI
	{
		#region Properties
		/// <summary>
		/// The private UIHelper.
		/// </summary>
		private static UIHelper helper = new UIHelper();
		#endregion

		#region Properties
		/// <summary>
		/// Gets if the current request is for a page.
		/// </summary>
		public static bool IsPage {
			get { return helper.IsPage; }
		}

		/// <summary>
		/// Gets if the current request is for a post.
		/// </summary>
		public static bool IsPost {
			get { return helper.IsPost; }
		}

		/// <summary>
		/// Gets if the current request is for the startpage.
		/// </summary>
		public static bool IsStart {
			get { return helper.IsStart; }
		}
		#endregion

		/// <summary>
		/// Gets the block with the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The block content</returns>
		public static IHtmlString Block(string slug) {
			return new HtmlString(helper.Block(slug));
		}

		/// <summary>
		/// Renders the head meta data from the currently requested content.
		/// </summary>
		/// <returns>The meta data</returns>
		public static IHtmlString Head() {
			return new HtmlString (helper.Head());
		}

		/// <summary>
		/// Renders the archive permalink for the given category.
		/// </summary>
		/// <param name="category">The category</param>
		/// <returns>The generated permalink</returns>
		public static IHtmlString Permalink(Piranha.Models.Category category) {
			return new HtmlString(helper.Permalink(category));
		}

		/// <summary>
		/// Renders the permalink for the given content.
		/// </summary>
		/// <param name="content">The content</param>
		/// <returns>The generated permalink</returns>
		public static IHtmlString Permalink(ContentModel content) {
			return new HtmlString(helper.Permalink(content));
		}

		/// <summary>
		/// Renders the permalink for the given content.
		/// </summary>
		/// <param name="content">The content model</param>
		/// <returns>The generated permalink</returns>
		public static IHtmlString Permalink(Piranha.Models.Content content) {
			return new HtmlString(helper.Permalink(content));
		}

		/// <summary>
		/// Generates a gravatar url for the given email and size.
		/// </summary>
		/// <param name="email">The email address</param>
		/// <param name="size">The size in pixels</param>
		/// <returns>The gravatar url</returns>
		public static IHtmlString GravatarUrl(string email, int size) {
			return new HtmlString(helper.GravatarUrl(email, size));
		}

		/// <summary>
		/// Renders the url to the given media file.
		/// </summary>
		/// <param name="media">The media file</param>
		/// <returns>The url</returns>
		public static IHtmlString Media(Piranha.Models.Media media, int? width, int? height) {
			return new HtmlString(helper.Media(media, width, height));
		}

		/// <summary>
		/// Renders the thumbnail url to the given media file.
		/// </summary>
		/// <param name="media">The media file</param>
		/// <returns>The url</returns>
		public static IHtmlString Thumbnail(Piranha.Models.Media media, int? size) {
			return new HtmlString(helper.Thumbnail(media, size));
		}

		/// <summary>
		/// Return the site structure as an ul/li list with the current page selected.
		/// </summary>
		/// <param name="start">The start level of the menu</param>
		/// <param name="stop">The stop level of the menu</param>
		/// <param name="levels">The number of levels. Use this if you don't know the start level</param>
		/// <param name="css">Optional css class for the outermost container</param>
		/// <returns>A rendered menu</returns>
		public static IHtmlString Menu(int start = 1, int stop = Int32.MaxValue, int levels = 0, string css = "menu") {
			return new HtmlString(helper.Menu(start, stop, levels, css));
		}
	}
}
