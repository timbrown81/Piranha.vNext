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
	/// Tests for the template repository.
	/// </summary>
	public abstract class TemplateTests
	{
		/// <summary>
		/// Tests the template repository.
		/// </summary>
		protected void Run() {
			var id = Guid.Empty;

			using (var api = new Api()) {
				// Add new model
				var model = new Models.Template() {
					Name = "Event",
					Type = Models.ContentType.Post,
					View = "Event"
				};
				model.Fields.Add(new Models.TemplateField() {
					InternalId = "Date",
					Name = "Event date",
					CLRType = typeof(Piranha.Extend.Components.Date).FullName
				});
				model.Fields.Add(new Models.TemplateField() {
					InternalId = "Venue",
					Name = "Venue",
					CLRType = typeof(Piranha.Extend.Components.String).FullName
				});
				api.Templates.Add(model);
				api.SaveChanges();

				id = model.Id;
			}

			using (var api = new Api()) {
				// Get model
				var model = api.Templates.GetSingle(id);

				Assert.IsNotNull(model);
				Assert.AreEqual("Event", model.Name);
				Assert.AreEqual(Models.ContentType.Post, model.Type);
				Assert.AreEqual("Event", model.View);
				Assert.AreEqual(2, model.Fields.Count);

				// Update model
				model.Name = "Updated";
				model.Fields.RemoveAt(0);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify update
				var model = api.Templates.GetSingle(id);

				Assert.IsNotNull(model);
				Assert.AreEqual("Updated", model.Name);
				Assert.AreEqual(Models.ContentType.Post, model.Type);
				Assert.AreEqual("Event", model.View);
				Assert.AreEqual(1, model.Fields.Count);

				// Remove model
				api.Templates.Remove(model);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = api.Templates.GetSingle(id);
				Assert.IsNull(model);
			}
		}
	}
}
