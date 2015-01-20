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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Piranha.Tests.Repositories
{
	/// <summary>
	/// Tests for the category repository.
	/// </summary>
	public abstract class CategoryTests
	{
		/// <summary>
		/// Test the category repository.
		/// </summary>
		protected void Run() {
			using (var api = new Api()) {
				// Add new model
				var model = new Models.Category() {
					Title = "My category"
				};
				api.Categories.Add(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Get model
				var model = api.Categories.GetSingle(where: c => c.Slug == "my-category");
				Assert.IsNotNull(model);

				// Update model
				model.Title = "Updated";
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.Categories.GetSingle(where: c => c.Slug == "my-category");
				Assert.IsNotNull(model);
				Assert.AreEqual("Updated", model.Title);

				// Remove model
				api.Categories.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.Categories.GetSingle(where: c => c.Slug == "my-category");
				Assert.IsNull(model);
			}
		}
	}
}
