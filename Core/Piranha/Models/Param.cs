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
	/// Params are used to store application parameter values.
	/// </summary>
	public sealed class Param : Model, Data.IModel, Data.IChanges
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the param name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the value of the param.
		/// </summary>
		public string Value { get; set; }

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
			App.ModelCache.Remove<Models.Param>(this.Id);
		}

		/// <summary>
		/// Called before the model is deleted by the DbContext.
		/// </summary>
		/// <param name="db">The current db context</param>
		public override void OnDelete() {
			// Remove from model cache
			App.ModelCache.Remove<Models.Param>(this.Id);
		}
		#endregion

		/// <summary>
		/// Method to validate model
		/// </summary>
		/// <returns>Returns the result of validation</returns>
		protected override FluentValidation.Results.ValidationResult Validate()
		{
			var validator = new ParamValidator();
			return validator.Validate(this);
		}

		#region Validator
		private class ParamValidator : AbstractValidator<Param>
		{
			public ParamValidator()
			{
				RuleFor(m => m.Name).NotEmpty();
				RuleFor(m => m.Name).Length(0, 128);
			}
		}
		#endregion
	}
}