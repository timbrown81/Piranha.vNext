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

namespace Piranha.Models
{
	/// <summary>
	/// Base class providing some internal events.
	/// </summary>
	public abstract class Model
	{
		/// <summary>
		/// Called when the model is materialized by the DbContext.
		/// </summary>
		public virtual void OnLoad() { }

		/// <summary>
		/// Called before the model is saved by the DbContext.
		/// </summary>
		public virtual void OnSave() {
			// ensure to validate the model as a first step
			try
			{
				// call validate on the model
				FluentValidation.Results.ValidationResult validationResults = this.Validate();

				if (!validationResults.IsValid)
				{
					throw new FluentValidation.ValidationException(validationResults.Errors);
				}
			}
			catch (NotImplementedException)
			{
				// if validate is not implemented, we ignore and move on.
			}

			
		}

		/// <summary>
		/// Called before the model is deleted by the DbContext.
		/// </summary>
		public virtual void OnDelete() { }

		/// <summary>
		/// Method to validate the model
		/// </summary>
		protected abstract FluentValidation.Results.ValidationResult Validate();
	}
}
