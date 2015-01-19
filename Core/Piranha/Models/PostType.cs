/*
 * Piranha CMS
 * Copyright (c) 2014, Håkan Edling, All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */

using System;
using System.Collections.Generic;
using FluentValidation;

namespace Piranha.Models
{
	/// <summary>
	/// Post types are used to define different kinds of posts.
	/// </summary>
	public sealed class PostType : Base.ContentType, Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets if posts of this type should be included
		/// in the site RSS or not.
		/// </summary>
		public bool IncludeInRss { get; set; }

		/// <summary>
		/// Gets/sets if archives should be enabled for this post type.
		/// </summary>
		public bool EnableArchive { get; set; }

		/// <summary>
		/// Gets/sets the meta keywords used when rendering the
		/// archive page for the post type.
		/// </summary>
		public string MetaKeywords { get; set; }

		/// <summary>
		/// Gets/sets the meta description used when rendering the
		/// archive page for the post type.
		/// </summary>
		public string MetaDescription { get; set; }

		/// <summary>
		/// Gets/sets the optional archive title.
		/// </summary>
		public string ArchiveTitle { get; set; }

		/// <summary>
		/// Gets/sets the optional route for the archive page
		/// for posts of this type.
		/// </summary>
		public string ArchiveRoute { get; set; }

		/// <summary>
		/// Gets/sets the optional view for the archive page
		/// for posts of this type.
		/// </summary>
		public string ArchiveView { get; set; }

		/// <summary>
		/// Gets/sets the optional route for commenting posts
		/// of this type.
		/// </summary>
		public string CommentRoute { get; set; }
		#endregion

		#region Internal events
		/// <summary>
		/// Called before the model is saved by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnSave() {
			// ensure to call the base class OnSave which will validate the model
			base.OnSave();

			// Remove from model cache
			App.ModelCache.Remove<PostType>(this.Id);
		}

		/// <summary>
		/// Called before the model is deleted by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnDelete() {
			// Remove from model cache
			App.ModelCache.Remove<PostType>(this.Id);
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostType() {
			EnableArchive = true;
		}

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate()
		{
			var validator = new PostTypeValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class PostTypeValidator : AbstractValidator<PostType>
		{
			public PostTypeValidator()
			{
				RuleFor(m => m.Name).NotEmpty();
				RuleFor(m => m.Name).Length(0, 128);
				RuleFor(m => m.Description).Length(0, 255);
				RuleFor(m => m.Route).Length(0, 255);
				RuleFor(m => m.View).Length(0, 255);
				RuleFor(m => m.ArchiveTitle).Length(0, 128);
				RuleFor(m => m.MetaKeywords).Length(0, 255);
				RuleFor(m => m.MetaDescription).Length(0, 255);
				RuleFor(m => m.ArchiveRoute).Length(0, 255);
				RuleFor(m => m.ArchiveView).Length(0, 255);
				RuleFor(m => m.CommentRoute).Length(0, 255);
				RuleFor(m => m.Slug).NotEmpty();
				RuleFor(m => m.Slug).Length(0, 128);

				// check unique Slug 
				using (var api = new Api())
				{
					RuleFor(m => m.Slug).Must((slug) => { return IsSlugUnique(slug, api); }).WithMessage("Slug should be unique");
				}
			}

			/// <summary>
			/// Function to validate if Slug is unique
			/// </summary>
			/// <param name="slug"></param>
			/// <param name="api"></param>
			/// <returns></returns>
			private bool IsSlugUnique(string slug, Api api)
			{
				var model = api.PostTypes.GetSingle(where: m => m.Slug == slug);
				return model == null;
			}
		}
		#endregion
	}
}