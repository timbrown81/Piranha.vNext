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
	/// Page types are used to define different kinds of pages.
	/// </summary>
	public sealed class PageType : Base.ContentType, Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets the available properties.
		/// </summary>
		public StateList<PageTypeProperty> Properties { get; set; }

		/// <summary>
		/// Gets/sets the available regions.
		/// </summary>
		public StateList<PageTypeRegion> Regions { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageType() {
			Properties = new StateList<PageTypeProperty>();
			Regions = new StateList<PageTypeRegion>();
		}

		#region Events
		/// <summary>
		/// Called before the model is saved by the DbContext.
		/// </summary>
		public override void OnSave() {
			// ensure to call the base class OnSave which will validate the model
			base.OnSave();

			// Order regions
			Regions = Regions.OrderBy(r => r.Order).ToStateList();

			// Ensure region id
			foreach (var reg in Regions)
				reg.Id = reg.Id == Guid.Empty ? Guid.NewGuid() : reg.Id;
		}
		#endregion

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate()
		{
			var validator = new PageTypeValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class PageTypeValidator : AbstractValidator<PageType>
		{
			public PageTypeValidator()
			{
				RuleFor(m => m.Name).NotEmpty();
				RuleFor(m => m.Name).Length(0, 128);
				RuleFor(m => m.Description).Length(0, 255);
				RuleFor(m => m.Route).Length(0, 255);
				RuleFor(m => m.View).Length(0, 255);
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
				var model = api.PageTypes.GetSingle(where: m => m.Slug == slug);
				return model == null;
			}
		}
		#endregion
	}
}