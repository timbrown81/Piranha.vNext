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
using System.Linq.Expressions;
using Piranha.Models;

namespace Piranha.Client.Models
{
	/// <summary>
	/// Application archive model.
	/// </summary>
	public class ArchiveModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the archive type.
		/// </summary>
		public ArchiveType Type { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the meta keywords.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the optional view.
		/// </summary>
		public string View { get; set; }

		/// <summary>
		/// Gets/sets the optionally requested year.
		/// </summary>
		public int? Year { get; set; }

		/// <summary>
		/// Gets/sets the optionally requested month.
		/// </summary>
		public int? Month { get; set; }

		/// <summary>
		/// Gets/sets the current page.
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// Gets/sets the total number of pages available.
		/// </summary>
		public int TotalPages { get; set; }

		/// <summary>
		/// Gets/sets the available posts.
		/// </summary>
		public IList<ContentModel> Posts { get; set; }
		#endregion

		#region Calculated properties
		/// <summary>
		/// Gets if the archive is for a specific year.
		/// </summary>
		public bool HasYear {
			get { return Year.HasValue;  }
		}

		/// <summary>
		/// Gets if the archive is for a specific year & month.
		/// </summary>
		public bool HasMonth {
			get { return HasYear && Month.HasValue; }
		}

		/// <summary>
		/// Gets if there's a previous page for the current archive.
		/// </summary>
		public bool HasPrev {
			get { return Page > 1; }
		}

		/// <summary>
		/// Gets if there's a next page for the current archive.
		/// </summary>
		public bool HasNext {
			get { return Page < TotalPages; }
		}

		/// <summary>
		/// Gets the href for the previous page in the current archive.
		/// </summary>
		public string LinkPrev {
			get {
				if (HasPrev) {
					return App.Env.Url("~/" + Slug +
						(HasYear ? "/" + Year.ToString() : "") +
						(HasMonth ? "/" + Month.ToString() : "") +
						(Page > 2 ? "/page/" + (Page - 1) : ""));
				}
				return "";
			}
		}

		/// <summary>
		/// Gets the href for the next page in the current archive.
		/// </summary>
		public string LinkNext {
			get {
				if (HasNext) {
					return App.Env.Url("~/" + Slug +
						(HasYear ? "/" + Year.ToString() : "") +
						(HasMonth ? "/" + Month.ToString() : "") +
						"/page/" + (Page + 1));
				}
				return "";
			}
		}
		#endregion

		/// <summary>
		/// Delegate for getting the current archive query.
		/// </summary>
		/// <returns>The archive query</returns>
		private delegate Expression<Func<Content, bool>> QueryDelegate();

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ArchiveModel() {
			Posts = new List<ContentModel>();
		}

		/// <summary>
		/// Gets the category archive that matches the given input.
		/// </summary>
		/// <param name="categoryId">The unique category id</param>
		/// <param name="page">The optional page</param>
		/// <param name="year">The optional year</param>
		/// <param name="month">The optional month</param>
		/// <returns>The model</returns>
		public static ArchiveModel GetCategoryArchive(Guid categoryId, int? page = 1, int? year = null, int? month = null) {
			return GetCategoryArchive<ArchiveModel>(categoryId, page, year, month);
		}

		/// <summary>
		/// Gets the category archive that matches the given input.
		/// </summary>
		/// <typeparam name="T">The archive type</typeparam>
		/// <param name="categoryId">The unique category id</param>
		/// <param name="page">The optional page</param>
		/// <param name="year">The optional year</param>
		/// <param name="month">The optional month</param>
		/// <returns>The model</returns>
		public static T GetCategoryArchive<T>(Guid categoryId, int? page = 1, int? year = null, int? month = null) where T : ArchiveModel {
			using (var api = new Api()) {
				var category = api.Categories.GetSingle(where: c => c.Id == categoryId);

				if (category != null) {
					return GetArchive<T>(api, ArchiveType.Category, category, () => {
						return GetCategoryQuery(categoryId, year, month);
					}, page, year, month);
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the post archive that matches the given input.
		/// </summary>
		/// <param name="page">The optional page</param>
		/// <param name="year">The optional year</param>
		/// <param name="month">The optional month</param>
		/// <returns>The model</returns>
		public static ArchiveModel GetPostArchive(int? page = 1, int? year = null, int? month = null) {
			return GetPostArchive<ArchiveModel>(page, year, month);
		}

		/// <summary>
		/// Gets the post archive that matches the given input.
		/// </summary>
		/// <typeparam name="T">The archive type</typeparam>
		/// <param name="page">The optional page</param>
		/// <param name="year">The optional year</param>
		/// <param name="month">The optional month</param>
		/// <returns>The model</returns>
		public static T GetPostArchive<T>(int? page = 1, int? year = null, int? month = null) where T : ArchiveModel {
			using (var api = new Api()) {
				return GetArchive<T>(api, ArchiveType.Post, PostArchive.Instance, () => {
					return GetPostQuery(year, month);
				}, page, year, month);
			}
		}

		/// <summary>
		/// Loads all available ratings for the posts in the archive.
		/// </summary>
		public virtual ArchiveModel WithRatings() {
			// Get all ratings
			using (var api = new Api()) {
				foreach (var post in Posts)
					post.Ratings = RatingsModel.GetByModelId(api, post.Id);
			}
			return this;
		}

		#region Private methods
		/// <summary>
		/// Gets the archive matching the given input.
		/// </summary>
		/// <typeparam name="T">The archive type</typeparam>
		private static T GetArchive<T>(Api api, ArchiveType type, IArchived archive, QueryDelegate getQuery, int? page = 1, int? year = null, int? month = null) where T : ArchiveModel {
			var model = Activator.CreateInstance<T>();

			model.Title = archive.ArchiveTitle;
			model.Slug = archive.ArchiveSlug;
			model.Keywords = archive.MetaKeywords;
			model.Description = archive.MetaDescription;
			model.View = archive.ArchiveView;

			model.Year = year;
			model.Month = month;
			model.Page = page.HasValue ? page.Value : 1;

			// Build query
			var query = getQuery();

			// Get content
			var content = api.Content.Get(where: query).Select(p => p.Id).ToList();
			var count = content.Count();

			// Get pages
			model.TotalPages = Math.Max(Convert.ToInt32(Math.Ceiling((double)count / Config.Site.ArchivePageSize)), 1);
			model.Page = Math.Min(model.Page, model.TotalPages);

			for (var n = (model.Page - 1) * Config.Site.ArchivePageSize; n < Math.Min(model.Page * Config.Site.ArchivePageSize, count); n++)
				model.Posts.Add(ContentModel.GetById(content[n]));

			return model;
		}

		/// <summary>
		/// Gets the post archive query.
		/// </summary>
		/// <param name="year">The optional year</param>
		/// <param name="month">The optional month</param>
		/// <returns>The query</returns>
		private static Expression<Func<Content, bool>> GetPostQuery(int? year, int? month) {
			Expression<Func<Content, bool>> query = null;
			var now = DateTime.Now;

			if (year.HasValue) {
				DateTime from;
				DateTime to;

				if (month.HasValue) {
					from = new DateTime(year.Value, month.Value, 1);
					to = from.AddMonths(1);
				} else {
					from = new DateTime(year.Value, 1, 1);
					to = from.AddYears(1);
				}
				query = p => p.Type == ContentType.Post && p.Category.IncludeInDefaultArchive && p.Published <= now && p.Published >= from && p.Published < to;
			} else {
				query = p => p.Type == ContentType.Post && p.Category.IncludeInDefaultArchive && p.Published <= now;
			}
			return query;
		}

		/// <summary>
		/// Gets the category archive query.
		/// </summary>
		/// <param name="id">The category id</param>
		/// <param name="year">The optional year</param>
		/// <param name="month">The optional month</param>
		/// <returns>The query</returns>
		private static Expression<Func<Content, bool>> GetCategoryQuery(Guid id, int? year, int? month) {
			Expression<Func<Content, bool>> query = null;
			var now = DateTime.Now;

			if (year.HasValue) {
				DateTime from;
				DateTime to;

				if (month.HasValue) {
					from = new DateTime(year.Value, month.Value, 1);
					to = from.AddMonths(1);
				} else {
					from = new DateTime(year.Value, 1, 1);
					to = from.AddYears(1);
				}
				query = p => p.Type == ContentType.Post && p.CategoryId == id && p.Published <= now && p.Published >= from && p.Published < to;
			} else {
				query = p => p.Type == ContentType.Post && p.CategoryId == id && p.Published <= now;
			}
			return query;
		}
		#endregion
	}
}
