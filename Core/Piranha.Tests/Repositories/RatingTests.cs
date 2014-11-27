/*
 * Piranha CMS
 * Copyright (c) 2014, Håkan Edling, All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Piranha.Tests.Repositories
{
	/// <summary>
	/// Tests for the rating repository.
	/// </summary>
	public abstract class RatingTests
	{
		/// <summary>
		/// Test the rating repository.
		/// </summary>
		protected void Run() {
			var userId = Guid.NewGuid().ToString();
			Models.PostType type = null;
			Models.Author author = null;
			Models.Post post = null;

			using (var api = new Api()) {
				// Add new post type
				type = new Models.PostType() {
					Name = "Rating post",
					Route = "post"
				};
				api.PostTypes.Add(type);
				api.SaveChanges();

				// Add new author
				author = new Models.Author() {
					Name = "Jim Doe",
					Email = "jim@doe.com"
				};
				api.Authors.Add(author);
				api.SaveChanges();

				// Add new post
				post = new Models.Post() {
					TypeId = type.Id,
					AuthorId = author.Id,
					Title = "My rated post",
					Excerpt = "Read my first post.",
					Body = "<p>Lorem ipsum</p>",
					Published = DateTime.Now
				};
				api.Posts.Add(post);
				api.SaveChanges();
			}
			
			using (var api = new Api()) {
				// Add ratings
				api.Ratings.AddRating(Models.RatingType.Star, post.Id, userId);
				api.Ratings.AddRating(Models.RatingType.Like, post.Id, userId);

				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify save
				var model = Client.Models.PostModel.GetById(post.Id).WithRatings();

				Assert.AreEqual(1, model.Ratings.Stars.Count);
				Assert.AreEqual(1, model.Ratings.Likes.Count);

				// Remove ratings
				api.Ratings.RemoveRating(Models.RatingType.Star, post.Id, userId);
				api.Ratings.RemoveRating(Models.RatingType.Like, post.Id, userId);

				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = Client.Models.PostModel.GetById(post.Id).WithRatings();

				Assert.AreEqual(0, model.Ratings.Stars.Count);
				Assert.AreEqual(0, model.Ratings.Likes.Count);

				// Remove
				api.Posts.Remove(post.Id);
				api.PostTypes.Remove(type.Id);
				api.Authors.Remove(author.Id);
				api.SaveChanges();
			}
			
			using (var api = new Api()) {
				// Verify remove
				post = api.Posts.GetSingle(where: p => p.Slug == "my-rated-post");
				type = api.PostTypes.GetSingle(type.Id);
				author = api.Authors.GetSingle(author.Id);

				Assert.IsNull(post);
				Assert.IsNull(type);
				Assert.IsNull(author);
			}
		}
	}
}
