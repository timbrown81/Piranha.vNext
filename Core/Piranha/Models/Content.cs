/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Piranha.Models
{
	/// <summary>
	/// The main content model.
	/// </summary>
	public class Content : Model, Data.IModel, Data.IChanges, Data.IPublishable
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the category id.
		/// </summary>
		public Guid CategoryId { get; set; }

		/// <summary>
		/// Gets/sets the optional template id.
		/// </summary>
		public Guid? TemplateId { get; set; }

		/// <summary>
		/// Gets/sets the optional author id.
		/// </summary>
		public Guid? AuthorId { get; set; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public ContentType Type { get; set; }

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
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets the publish date.
		/// </summary>
		public DateTime? Published { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the optional author.
		/// </summary>
		public Author Author { get; set; }

		/// <summary>
		/// Gets/sets the category,
		/// </summary>
		public Category Category { get; set; }

		/// <summary>
		/// Gets/sets the optional template.
		/// </summary>
		public Template Template { get; set; }

		/// <summary>
		/// Gets/sets the main content body.
		/// </summary>
		public IList<ContentRow> Body { get; set; }

		/// <summary>
		/// Gets/sets the available template fields.
		/// </summary>
		public IList<ContentField> Fields { get; set; }

		/// <summary>
		/// Gets/sets the available comments.
		/// </summary>
		public IList<Comment> Comments { get; set; }

		/// <summary>
		/// Gets/sets the available tags.
		/// </summary>
		public IList<Tag> Tags { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Content() {
			Body = new List<ContentRow>();
			Fields = new List<ContentField>();
			Comments = new List<Comment>();
			Tags = new List<Tag>();
		}

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate() {
			return new ContentValidator().Validate(this);
		}

		#region Validator
		/// <summary>
		/// Validateor for the content model.
		/// </summary>
		private class ContentValidator : AbstractValidator<Content>
		{
			/// <summary>
			/// Default constructor.
			/// </summary>
			public ContentValidator() {
				RuleFor(m => m.Title).NotEmpty().WithMessage("Title is required");
				RuleFor(m => m.Title).Length(0, 128).WithMessage("Title has a maximum length of 128 characters");
				RuleFor(m => m.MetaKeywords).Length(0, 128).WithMessage("Meta keywords has a maximum length of 128 characters");
				RuleFor(m => m.MetaDescription).Length(0, 255).WithMessage("Meta description has a maximum length of 255 characters");
				RuleFor(m => m.Slug).Must((m, slug) => { return ValidateSlug(m); }).WithMessage("Slug should be unique");
			}

			/// <summary>
			/// Function to validate the slug.
			/// </summary>
			/// <param name="model">The model</param>
			/// <returns>If the slug was unique</returns>
			private bool ValidateSlug(Content model) {
				using (var api = new Api()) {
					return api.Content.Get(where: c => c.Type == model.Type && c.Slug == model.Slug && c.Id != model.Id).Count() == 0;
				}
			}
		}
		#endregion
	}
}
