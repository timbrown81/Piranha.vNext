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
	/// Media contains all of the meta data for uploaded files.
	/// </summary>
	public sealed class Media : Model, Data.IModel, Data.IChanges
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
		/// gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the content type of the uploaded media.
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Gets/sets the length in bytes for the uploaded media.
		/// </summary>
		public long ContentLength { get; set; }

		/// <summary>
		/// Gets/sets if the media is an image or not.
		/// </summary>
		public bool IsImage { get; set; }

		/// <summary>
		/// Gets/sets the width if the media is an image.
		/// </summary>
		public int? Width { get; set; }

		/// <summary>
		/// Gets/sets the height if the media is an image.
		/// </summary>
		public int? Height { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }
		#endregion

		#region Events
		/// <summary>
		/// Called before the model is saved by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnSave() {
			// ensure to call the base class OnSave which will validate the model
			base.OnSave();

			// Remove from model cache
			App.ModelCache.Remove<Models.Media>(Id);
		}

		/// <summary>
		/// Called before the model is deleted by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnDelete() {
			// Remove from model cache
			App.ModelCache.Remove<Models.Media>(Id);

			// Remove binary data
			App.Media.Delete(this);
		}
		#endregion

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate()
		{
			var validator = new MediaValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class MediaValidator : AbstractValidator<Media>
		{
			public MediaValidator()
			{
				RuleFor(m => m.Name).NotEmpty();
				RuleFor(m => m.Name).Length(0, 128);
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
				var model = api.Media.GetSingle(where: m => m.Slug == slug);
				return model == null;
			}
		}
		#endregion
	}
}
