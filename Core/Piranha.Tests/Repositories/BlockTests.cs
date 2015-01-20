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
	/// Tests for the block repository.
	/// </summary>
	public abstract class BlockTests
	{
		/// <summary>
		/// Test the category repository.
		/// </summary>
		protected void Run() {
			using (var api = new Api()) {
				// Add new model
				var model = new Models.Block() {
					Name = "My block",
					Description = "My test block",
					Body = "Lorem ipsum"
				};
				api.Blocks.Add(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Get model
				var model = api.Blocks.GetSingle(where: c => c.Slug == "my-block");
				Assert.IsNotNull(model);
				Assert.AreEqual("My block", model.Name);
				Assert.AreEqual("My test block", model.Description);
				Assert.AreEqual("Lorem ipsum", model.Body);

				// Update model
				model.Name = "Updated";
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.Blocks.GetSingle(where: c => c.Slug == "my-block");
				Assert.IsNotNull(model);
				Assert.AreEqual("Updated", model.Name);
				Assert.AreEqual("My test block", model.Description);
				Assert.AreEqual("Lorem ipsum", model.Body);

				// Remove model
				api.Blocks.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.Blocks.GetSingle(where: c => c.Slug == "my-block");
				Assert.IsNull(model);
			}
		}
	}
}
