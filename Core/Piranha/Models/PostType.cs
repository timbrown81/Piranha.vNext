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
				RuleFor(m => m.Slug).Must((model, slug) => { return IsSlugUnique(slug, model.Id); }).WithMessage("Slug should be unique");
			}

			/// <summary>
			/// Function to validate if Slug is unique
			/// </summary>
			/// <param name="slug"></param>
			/// <param name="api"></param>
			/// <returns></returns>
			private bool IsSlugUnique(string slug, Guid id)
			{
				using (var api = new Api())
				{
					var recordCount = api.PostTypes.Get(where: m => m.Slug == slug && m.Id !=id).Count();
					return recordCount == 0;	
				}
			}
		}
		#endregion
	}
}