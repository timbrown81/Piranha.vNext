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
	/// The main application content model.
	/// </summary>
	public class ContentModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public ContentType Type { get; set; }

		/// <summary>
		/// Gets/sets the template name.
		/// </summary>
		public string Template { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the optional meta keywords.
		/// </summary>
		public string MetaKeywords { get; set; }

		/// <summary>
		/// Gets/sets the optional meta description.
		/// </summary>
		public string MetaDescription { get; set; }

		/// <summary>
		/// Gets/sets the short excerpt.
		/// </summary>
		public string Excerpt { get; set; }

		/// <summary>
		/// Gets/sets the main content body.
		/// </summary>
		public IList<ContentRow> Body { get; set; }

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
		/// Gets/sets the number of available comments.
		/// </summary>
		public int CommentCount { get; set; }

		/// <summary>
		/// Gets/sets the author responsible for this content.
		/// </summary>
		public Author Author { get; set; }

		/// <summary>
		/// Gets/sets the category,
		/// </summary>
		public Category Category { get; set; }
	
		/// <summary>
		/// Gets/sets the available comments.
		/// </summary>
		public IList<CommentModel> Comments { get; set; }

		/// <summary>
		/// Gets/sets the available ratings.
		/// </summary>
		public RatingsModel Ratings { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ContentModel() {
			Body = new List<ContentRow>();
			Comments = new List<CommentModel>();
			Ratings = new RatingsModel();
		}

		/// <summary>
		/// Gets the content model identified by the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public static ContentModel GetById(Guid id) {
			return GetById<ContentModel>(id);
		}

		/// <summary>
		/// Gets the content model identified by the given id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public static T GetById<T>(Guid id) where T : ContentModel {
			using (var api = new Api()) {
				var content = api.Content.GetSingle(id);

				if (content != null && content.Published <= DateTime.Now) {
					var model = Map<T>(content);
					model.CommentCount = api.Comments.Get(where: c => c.ContentId == content.Id && c.IsApproved && !c.IsSpam).Count();

					return model;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the content model identified by the given slug.
		/// </summary>
		/// <param name="type">The content type</param>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		public static ContentModel GetBySlug(ContentType type, string slug) {
			return GetBySlug<ContentModel>(type, slug);
		}

		/// <summary>
		/// Gets the content model identified by the given slug.
		/// </summary>
		/// <param name="type">The content type</param>
		/// <param name="slug">The unique slug</param>
		/// <returns>The model</returns>
		public static T GetBySlug<T>(ContentType type, string slug) where T : ContentModel {
			using (var api = new Api()) {
				var content = api.Content.GetSingle(where: c => c.Type == type && c.Slug == slug);

				if (content != null && content.Published <= DateTime.Now) {
					var model = Map<T>(content);
					model.CommentCount = api.Comments.Get(where: c => c.ContentId == content.Id && c.IsApproved && !c.IsSpam).Count();

					return model;
				}
			}
			return null;
		}

		/// <summary>
		/// Loads all available comments for the current content.
		/// </summary>
		/// <param name="ratings">If ratings should be included</param>
		public virtual ContentModel WithComments(bool ratings = false) {
			// Get all comments
			using (var api = new Api()) {
				Comments = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentModel>>(api.Comments.Get(where: c => c.ContentId == Id)).ToList();

				if (ratings) {
					foreach (var comment in Comments)
						comment.Ratings = RatingsModel.GetByModelId(api, comment.Id);
				}
			}
			return this;
		}

		/// <summary>
		/// Loads all available ratings for the current post.
		/// </summary>
		public virtual ContentModel WithRatings() {
			// Get all ratings
			using (var api = new Api()) {
				Ratings = RatingsModel.GetByModelId(api, Id);
			}
			return this;
		}

		#region Private methods
		/// <summary>
		/// Maps the given content to a new content model.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="content">The content</param>
		/// <returns>The content model</returns>
		private static T Map<T>(Content content) where T : ContentModel {
			if (content != null) {
				var model = Activator.CreateInstance<T>();

				Mapper.Map<Content, ContentModel>(content, model);

				model.Route = content.Template != null && !String.IsNullOrEmpty(content.Template.Route) ? content.Template.Route : "content";
				model.View = content.Template != null && !String.IsNullOrEmpty(content.Template.View) ? content.Template.View : "";

				return model;
			}
			return null;
		}
		#endregion
	}
}
