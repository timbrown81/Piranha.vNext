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
	public class ContentModel
	{
		#region Properties
		public Guid Id { get; set; }
		public ContentType Type { get; set; }
		public string Template { get; set; }
		public string Title { get; set; }
		public string Slug { get; set; }
		public string MetaKeywords { get; set; }
		public string MetaDescription { get; set; }
		public IList<ContentRow> Body { get; set; }
		public string Route { get; set; }
		public string View { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public DateTime Published { get; set; }
		public Author Author { get; set; }
		public IList<Category> Categories { get; set; }
		public IList<Comment> Comments { get; set; }
		public RatingsModel Ratings { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ContentModel() {
			Body = new List<ContentRow>();
			Categories = new List<Category>();
			Comments = new List<Comment>();
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
				var model = api.Content.GetSingle(id);

				if (model != null && model.Published <= DateTime.Now) {
					return Map<T>(model);
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the content model identified by the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="type">The content type</param>
		/// <returns>The model</returns>
		public static ContentModel GetBySlug(string slug, ContentType type) {
			return GetBySlug<ContentModel>(slug, type);
		}

		/// <summary>
		/// Gets the content model identified by the given slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <param name="type">The content type</param>
		/// <returns>The model</returns>
		public static T GetBySlug<T>(string slug, ContentType type) where T : ContentModel {
			using (var api = new Api()) {
				var content = api.Content.GetSingle(where: c => c.Type == type && c.Slug == slug);

				if (content != null && content.Published <= DateTime.Now) {
					return Map<T>(content);
				}
			}
			return null;
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
