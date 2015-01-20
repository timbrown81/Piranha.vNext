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
	/// Authors are used to sign content.
	/// </summary>
	public sealed class Author : Model, Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		public string Description { get; set; }

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
			var validator = new AuthorValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class AuthorValidator : AbstractValidator<Author>
		{
			public AuthorValidator()
			{
				RuleFor(m => m.Name).NotEmpty();
				RuleFor(m => m.Name).Length(0, 128);
				RuleFor(m => m.Email).Length(0,128);
				RuleFor(m => m.Description).Length(0,512);
			}
		}
		#endregion
	}
}
