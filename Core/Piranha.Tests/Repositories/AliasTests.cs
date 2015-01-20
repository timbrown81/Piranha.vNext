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
	/// Tests for the alias repository.
	/// </summary>
	public abstract class AliasTests
	{
		/// <summary>
		/// Test the alias repository.
		/// </summary>
		protected void Run() {
			var id = Guid.Empty;

			using (var api = new Api()) {
				// Add new model
				var model = new Models.Alias() {
					OldUrl = "oldstuff.aspx?id=thisisalongunreadableandyglyurl",
					NewUrl = "~/blog/my-new-permalink",
					IsPermanent = true
				};
				api.Aliases.Add(model);
				api.SaveChanges();
				id = model.Id;
			}

			using (var api = new Api()) {
				// Get model
				var model = api.Aliases.GetSingle(id);
				Assert.IsNotNull(model);
				Assert.AreEqual("/oldstuff.aspx?id=thisisalongunreadableandyglyurl", model.OldUrl);
				Assert.AreEqual("/blog/my-new-permalink", model.NewUrl);
				Assert.AreEqual(true, model.IsPermanent);

				// Update model
				model.NewUrl = "/blog/welcome";
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.Aliases.GetSingle(id);
				Assert.IsNotNull(model);
				Assert.AreEqual("/oldstuff.aspx?id=thisisalongunreadableandyglyurl", model.OldUrl);
				Assert.AreEqual("/blog/welcome", model.NewUrl);
				Assert.AreEqual(true, model.IsPermanent);

				// Remove model
				api.Aliases.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.Aliases.GetSingle(id);
				Assert.IsNull(model);
			}
		}
	}
}
