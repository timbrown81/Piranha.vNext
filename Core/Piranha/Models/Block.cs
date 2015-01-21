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
	/// Blocks are site global reusable content blocks.
	/// </summary>
	public sealed class Block : Model, Data.IModel, Data.IChanges
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
		/// Gets/sets the unique slug.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the main body.
		/// </summary>
		public string Body { get; set; }

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
		/// Called when the model is materialized by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnLoad() {
			if (Hooks.Models.Block.OnLoad != null)
				Hooks.Models.Block.OnLoad(this);
		}

		/// <summary>
		/// Called before the model is saved by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnSave() {
			// ensure to call the base class OnSave which will validate the model
			base.OnSave();

			if (Hooks.Models.Block.OnSave != null)
				Hooks.Models.Block.OnSave(this);

			// Remove from model cache
			App.ModelCache.Remove<Models.Block>(this.Id);
		}

		/// <summary>
		/// Called before the model is deleted by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnDelete() {
			if (Hooks.Models.Block.OnDelete != null)
				Hooks.Models.Block.OnDelete(this);

			// Remove from model cache
			App.ModelCache.Remove<Models.Block>(this.Id);
		}
		#endregion

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate()
		{
			var validator = new BlockValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class BlockValidator : AbstractValidator<Block>
		{
			public BlockValidator()
			{
				RuleFor(m => m.Name).NotEmpty();
				RuleFor(m => m.Name).Length(0, 128);
				RuleFor(m => m.Description).Length(0, 255);
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
				using (var api = new Api()) {
					var recordCount = api.Blocks.Get(where: m => m.Slug == slug && m.Id != id).Count();
					return recordCount == 0;
				}
			}
		}
		#endregion
	}
}
