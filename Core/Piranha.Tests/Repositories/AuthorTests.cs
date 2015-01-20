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
	/// Tests for the author repository.
	/// </summary>
	public abstract class AuthorTests
	{
		/// <summary>
		/// Test the author repository.
		/// </summary>
		protected void Run() {
			using (var api = new Api()) {
				// Add new model
				var model = new Models.Author() {
					Name = "John Doe",
					Email = "john@doe.com"
				};
				api.Authors.Add(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Get model
				var model = api.Authors.GetSingle(where: a => a.Name == "John Doe");

				Assert.IsNotNull(model);
				Assert.AreEqual("john@doe.com", model.Email);

				// Update model
				model.Name = "Sir John Doe";
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.Authors.GetSingle(where: a => a.Email == "john@doe.com");

				Assert.IsNotNull(model);
				Assert.AreEqual("Sir John Doe", model.Name);

				// Remove model
				api.Authors.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.Authors.GetSingle(where: a => a.Email == "john@doe.com");
				Assert.IsNull(model);
			}
		}
	}
}
