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
	/// Categories are used to group posts together.
	/// </summary>
	public sealed class Category : Model, Data.IModel, Data.IChanges
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
		protected override FluentValidation.Results.ValidationResult Validate()
		{
			var validator = new CategoryValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class CategoryValidator : AbstractValidator<Category>
		{
			public CategoryValidator()
			{
				RuleFor(m => m.Title).NotEmpty();
				RuleFor(m => m.Title).Length(0, 128);
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
					var recordCount = api.Categories.Get(where: m => m.Slug == slug && m.Id != id).Count();
					return recordCount == 0;
				}
			}
		}
		#endregion
	}
}