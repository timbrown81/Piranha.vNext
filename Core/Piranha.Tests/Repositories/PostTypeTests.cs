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
	/// Tests for the post type repository.
	/// </summary>
	public abstract class PostTypeTests
	{
		/// <summary>
		/// Test the post type repository.
		/// </summary>
		protected void Run() {
			using (var api = new Api()) {
				// Add new model
				var model = new Models.PostType() {
					Name = "Standard post",
					Route = "post"
				};
				api.PostTypes.Add(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Get model
				var model = api.PostTypes.GetSingle(where: t => t.Slug == "standard-post");

				Assert.IsNotNull(model);
				Assert.AreEqual("Standard post", model.Name);
				Assert.AreEqual("post", model.Route);

				// Update model
				model.Name = "Updated post";
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.PostTypes.GetSingle(where: t => t.Slug == "standard-post");

				Assert.IsNotNull(model);
				Assert.AreEqual("Updated post", model.Name);
				Assert.AreEqual("post", model.Route);

				// Remove model
				api.PostTypes.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.PostTypes.GetSingle(where: t => t.Slug == "standard-post");
				Assert.IsNull(model);
			}
		}
	}
}
