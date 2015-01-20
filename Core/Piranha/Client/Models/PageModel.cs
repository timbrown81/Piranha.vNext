/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Piranha.Models;

namespace Piranha.Client.Models
{
	/// <summary>
	/// Application page model.
	/// </summary>
	public class PageModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the slug of the page type.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets/sets the optional parent id.
		/// </summary>
		public Guid? ParentId { get; set; }

		/// <summary>
		/// Gets/sets the sort order of the page.
		/// </summary>
		public int SortOrder { get; set; }

		/// <summary>
		/// Gets/sets if the page should be hidden from navigations.
		/// </summary>
		public bool IsHidden { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the optional keywords.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the main page body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Gets/sets the optional route that should handle requests.
		/// </summary>
		public string Route { get; set; }

		/// <summary>
		/// Gets/sets the optional view that should render requests.
		/// </summary>
		public string View { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets when the model was published.
		/// </summary>
		public DateTime Published { get; set; }

		/// <summary>
		/// Gets/sets the author responsible for this content.
		/// </summary>
		public Author Author { get; set; }

		/// <summary>
		/// Gets/sets the available ratings.
		/// </summary>
		public RatingsModel Ratings { get; set; }

		/// <summary>
		/// Gets if this is the startpage of the site.
		/// </summary>
		public bool IsStartPage {
			get { return !ParentId.HasValue && SortOrder == 1; }
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageModel() {
			Ratings = new RatingsModel();
		}

		/// <summary>
		/// Gets the page model identified by the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public static PageModel GetById(Guid id) {
			return GetById<PageModel>(id);
		}

		/// <summary>
		/// Gets the page model identified by the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public static T GetById<T>(Guid id) where T : PageModel {
			var model = (T)App.ModelCache.GetById<PageModel>(id);

			if (model == null) {
				using (var api = new Api()) {
					var now = DateTime.Now;

					model = Map<T>(api, api.Pages.GetSingle(where: p => p.Id == id && p.Published <= now));

					if (model != null)
						App.ModelCache.Add<PageModel>(model);
				}
			}
			return model;
		}

		/// <summary>
		/// Gets the page model identified by the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		public static PageModel GetBySlug(string slug) {
			return GetBySlug<PageModel>(slug);
		}

		/// <summary>
		/// Gets the page model identified by the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		public static T GetBySlug<T>(string slug) where T : PageModel {
			var model = (T)App.ModelCache.GetByKey<PageModel>(slug);

			if (model == null) {
				using (var api = new Api()) {
					var now = DateTime.Now;

					model = Map<T>(api, api.Pages.GetSingle(where: p => p.Slug == slug && p.Published <= now));

					if (model != null)
						App.ModelCache.Add<PageModel>(model);
				}
			}
			return model;
		}

		/// <summary>
		/// Loads all available ratings for the current page.
		/// </summary>
		public virtual PageModel WithRatings() {
			// Get all ratings
			using (var api = new Api()) {
				Ratings = RatingsModel.GetByModelId(api, Id);
			}
			return this;
		}

		/// <summary>
		/// Gets the last modification date for the curremt post model.
		/// </summary>
		public virtual DateTime GetLastModified() {
			if (Updated > Published)
				return Updated;
			return Published;
		}

		#region Private methods
		/// <summary>
		/// Maps the given page to a new page model.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="page">The page</param>
		/// <returns>The page model</returns>
		private static T Map<T>(Api api, Page page) where T : PageModel {
			if (page != null) {
				var model = Activator.CreateInstance<T>();

				Mapper.Map<Page, PageModel>(page, model);

				return model;
			}
			return null;
		}
		#endregion
	}
}
