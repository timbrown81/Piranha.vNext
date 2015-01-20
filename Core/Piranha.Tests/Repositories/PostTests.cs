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
	/// Tests for the post repository.
	/// </summary>
	public abstract class PostTests
	{
		/// <summary>
		/// Test the post repository.
		/// </summary>
		protected void Run() {
			Models.PostType type = null;
			Models.Author author = null;
			Models.Post post = null;

			using (var api = new Api()) {
				// Add new post type
				type = new Models.PostType() {
					Name = "Test post",
					Route = "post"
				};
				api.PostTypes.Add(type);
				api.SaveChanges();

				// Add new author
				author = new Models.Author() {
					Name = "Jane Doe",
					Email = "jane@doe.com"
				};
				api.Authors.Add(author);
				api.SaveChanges();

				// Add new post
				post = new Models.Post() {
					TypeId = type.Id,
					AuthorId = author.Id,
					Title = "My test post",
					Excerpt = "Read my first post.",
					Body = "<p>Lorem ipsum</p>",
					Published = DateTime.Now
				};
				api.Posts.Add(post);
				api.SaveChanges();
			}
			
			using (var api = new Api()) {
				// Get model
				var model = api.Posts.GetSingle(where: p => p.Slug == "my-test-post");

				Assert.IsNotNull(model);
				Assert.AreEqual("Read my first post.", model.Excerpt);
				Assert.AreEqual("<p>Lorem ipsum</p>", model.Body);

				// Update model
				model.Excerpt = "Updated post";
				api.SaveChanges();
			}
			
			using (var api = new Api()) {
				// Verify update
				var model = api.Posts.GetSingle(where: p => p.Slug == "my-test-post");

				Assert.IsNotNull(model);
				Assert.AreEqual("Updated post", model.Excerpt);
				Assert.AreEqual("<p>Lorem ipsum</p>", model.Body);

				// Remove
				api.Posts.Remove(model);
				api.PostTypes.Remove(type.Id);
				api.Authors.Remove(author.Id);
				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				post = api.Posts.GetSingle(where: p => p.Slug == "my-test-post");
				type = api.PostTypes.GetSingle(type.Id);
				author = api.Authors.GetSingle(author.Id);

				Assert.IsNull(post);
				Assert.IsNull(type);
				Assert.IsNull(author);
			}
		}
	}
}
