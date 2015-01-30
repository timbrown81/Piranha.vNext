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
	/// Tests for the rating repository.
	/// </summary>
	public abstract class RatingTests
	{
		/// <summary>
		/// Test the rating repository.
		/// </summary>
		protected void Run() {
			var userId = Guid.NewGuid().ToString();
			Models.Author author = null;
			Models.Category category = null;
			Models.Content content = null;

			using (var api = new Api()) {
				// Add new author
				author = new Models.Author() {
					Name = "Jim Doe",
					Email = "jim@doe.com"
				};
				api.Authors.Add(author);

				// Add new category
				category = new Models.Category() {
					Title = "Uncategorized"
				};
				api.Categories.Add(category);
				api.SaveChanges();

				// Add new content
				content = new Models.Content() {
					Type = Models.ContentType.Post,
					CategoryId = category.Id,
					AuthorId = author.Id,
					Title = "My rated post",
					Published = DateTime.Now
				};
				api.Content.Add(content);
				api.SaveChanges();
			}
			
			using (var api = new Api()) {
				// Add ratings
				api.Ratings.AddRating(Models.RatingType.Star, content.Id, userId);
				api.Ratings.AddRating(Models.RatingType.Like, content.Id, userId);

				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify save
				var model = Client.Models.ContentModel.GetById(content.Id).WithRatings();

				Assert.AreEqual(1, model.Ratings.Stars.Count);
				Assert.AreEqual(1, model.Ratings.Likes.Count);

				// Remove ratings
				api.Ratings.RemoveRating(Models.RatingType.Star, content.Id, userId);
				api.Ratings.RemoveRating(Models.RatingType.Like, content.Id, userId);

				api.SaveChanges();
			}

			using (var api = new Api()) {
				// Verify remove
				var model = Client.Models.ContentModel.GetById(content.Id).WithRatings();

				Assert.AreEqual(0, model.Ratings.Stars.Count);
				Assert.AreEqual(0, model.Ratings.Likes.Count);

				// Remove
				api.Categories.Remove(category.Id);
				api.Content.Remove(content.Id);
				api.Authors.Remove(author.Id);
				api.SaveChanges();
			}
			
			using (var api = new Api()) {
				// Verify remove
				category = api.Categories.GetSingle(category.Id);
				content = api.Content.GetSingle(where: p => p.Slug == "my-rated-post");
				author = api.Authors.GetSingle(author.Id);

				Assert.IsNull(category);
				Assert.IsNull(content);
				Assert.IsNull(author);
			}
		}
	}
}
