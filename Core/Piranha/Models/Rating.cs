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
	/// Ratings are used to star, like & dislike content.
	/// </summary>
	public sealed class Rating : Model, Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the id of the model this is a rating for.
		/// </summary>
		public Guid ModelId { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who added the rating.
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Gets/sets the rating type.
		/// </summary>
		public RatingType Type { get; set; }

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
			var validator = new RatingValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class RatingValidator : AbstractValidator<Rating>
		{
			public RatingValidator()
			{
				RuleFor(m => m.UserId).NotEmpty();
				RuleFor(m => m.UserId).Length(0, 128);
			}
		}
		#endregion
	}
}
