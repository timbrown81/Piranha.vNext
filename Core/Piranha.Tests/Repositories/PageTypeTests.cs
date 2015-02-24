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
	/// Tests for the page type repository.
	/// </summary>
	public abstract class PageTypeTests
	{
		/// <summary>
		/// Test the page type repository.
		/// </summary>
		protected void Run() {
			using (var api = new Api()) {
				// Add new model
				var model = new Models.PageType() {
					Name = "Standard page",
					Route = "page"
				};
				api.PageTypes.Add(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Get model
				var model = api.PageTypes.GetSingle(where: t => t.Slug == "standard-page");

				Assert.IsNotNull(model);
				Assert.AreEqual("Standard page", model.Name);
				Assert.AreEqual("page", model.Route);

				// Update model
				model.Name = "Updated page";
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.PageTypes.GetSingle(where: t => t.Slug == "standard-page");

				Assert.IsNotNull(model);
				Assert.AreEqual("Updated page", model.Name);
				Assert.AreEqual("page", model.Route);

				// Remove model
				api.PageTypes.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.PageTypes.GetSingle(where: t => t.Slug == "standard-page");
				Assert.IsNull(model);
			}
		}
	}
}
