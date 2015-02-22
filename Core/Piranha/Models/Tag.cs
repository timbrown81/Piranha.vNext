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
using System.Linq;
using FluentValidation;

namespace Piranha.Models
{
	/// <summary>
	/// Tags are used to classify content.
	/// </summary>
	public sealed class Tag : Model, IArchived, Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the archive title.
		/// </summary>
		public string ArchiveTitle { get; set; }

		/// <summary>
		/// Gets the archive slug.
		/// </summary>
		public string ArchiveSlug {
			get { return Config.Permalinks.TagArchiveSlug + "/" + Slug; }
		}

		/// <summary>
		/// Gets/sets the optional archive meta keywords.
		/// </summary>
		public string MetaKeywords { get; set; }

		/// <summary>
		/// Gets/sets the optional archive meta description.
		/// </summary>
		public string MetaDescription { get; set; }

		/// <summary>
		/// Gets/sets the optional archive view.
		/// </summary>
		public string ArchiveView { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }
		#endregion

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate() {
			return new TagValidator().Validate(this);
		}

		#region Validator
		/// <summary>
		///  Validator for the tag
		/// </summary>
		private class TagValidator : AbstractValidator<Tag>
		{
			public TagValidator() {
				RuleFor(m => m.Title).NotEmpty();
				RuleFor(m => m.Title).Length(0, 128);
				RuleFor(m => m.Slug).NotEmpty();
				RuleFor(m => m.Slug).Length(0, 128);
				RuleFor(m => m.ArchiveTitle).Length(0, 128);
				RuleFor(m => m.ArchiveView).Length(0, 255);
				RuleFor(m => m.MetaKeywords).Length(0, 255);
				RuleFor(m => m.MetaDescription).Length(0, 255);
				RuleFor(m => m.Slug).Must((model, slug) => { 
					return IsSlugUnique(model); 
				}).WithMessage("Slug should be unique");
			}

			/// <summary>
			/// Validates if the slug is unique in the database.
			/// </summary>
			/// <param name="model">The current model</param>
			/// <returns>If it is unique</returns>
			private bool IsSlugUnique(Tag model) {
				using (var api = new Api()) {
					return api.Tags.Get(where: m => m.Slug == model.Slug && m.Id != model.Id).Count() == 0;
				}
			}
		}
		#endregion
	}
}