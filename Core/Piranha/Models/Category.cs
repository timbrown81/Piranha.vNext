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
				var model = api.Categories.GetSingle(where: m => m.Slug == slug);
				return model == null;
			}
		}
		#endregion
	}
}