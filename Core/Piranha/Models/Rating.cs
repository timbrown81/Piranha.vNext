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
