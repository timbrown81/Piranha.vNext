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
	/// Regions are used to define the properties available 
	/// for a certain page type.
	/// </summary>
	public sealed class PageTypeProperty : Base.ContentTypePart, Data.IModel 
	{
		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate()
		{
			var validator = new PageTypePropertyValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class PageTypePropertyValidator : AbstractValidator<PageTypeProperty>
		{
			public PageTypePropertyValidator()
			{
				RuleFor(m => m.Name).NotEmpty();
				RuleFor(m => m.Name).Length(0, 128);
				RuleFor(m => m.CLRType).NotEmpty();
				RuleFor(m => m.CLRType).Length(0, 512);
			}
		}
		#endregion
	}
}
