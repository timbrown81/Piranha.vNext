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
	/// Tests for the param repository.
	/// </summary>
	public abstract class ParamTests
	{
		/// <summary>
		/// Test the param repository.
		/// </summary>
		protected void Run() {
			using (var api = new Api()) {
				// Add new model
				var model = new Models.Param() {
					Name = "my_param",
					Value = "23"
				};
				api.Params.Add(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Get model
				var model = api.Params.GetSingle(where: p => p.Name == "my_param");

				Assert.IsNotNull(model);
				Assert.AreEqual("23", model.Value);

				// Update model
				model.Value = "32";
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.Params.GetSingle(where: p => p.Name == "my_param");

				Assert.IsNotNull(model);
				Assert.AreEqual("32", model.Value);

				// Remove model
				api.Params.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.Params.GetSingle(where: p => p.Name == "my_param");
				Assert.IsNull(model);
			}
		}
	}
}
