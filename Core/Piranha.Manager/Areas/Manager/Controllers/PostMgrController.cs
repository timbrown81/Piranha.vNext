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
using System.Linq;
using System.Web.Mvc;
using Piranha.Manager.Models.Post;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing posts.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix="manager")]
    public class PostMgrController : ManagerController
    {
		/// <summary>
		/// Gets a list of the currently available posts
		/// </summary>
		/// <returns></returns>
		[Route("posts/{slug?}")]
		public ActionResult List(string slug = null) {
			return View(ListModel.Get(slug));
		}

		[Route("post/add/{type}")]
		public ActionResult Add(string type) {
			var model = new EditModel(api, type);

			ViewBag.Title = String.Format(Piranha.Manager.Resources.Post.AddTitle, model.TypeName);
			return View("Edit", model);
		}

		/// <summary>
		/// Gets the edit view for a new or existing post.
		/// </summary>
		/// <param name="id">The optional id</param>
		/// <returns>The view result</returns>
		[Route("post/edit/{id:Guid}")]
		public ActionResult Edit(Guid id) {
			var model = EditModel.GetById(api, id);

			ViewBag.Title = Piranha.Manager.Resources.Post.EditTitle;
			ViewBag.SubTitle = model.Published.HasValue ? 
				((model.Published.Value > DateTime.Now ? "Schedueled: " : "Published: ") + model.Published.Value.ToString("yyyy-MM-dd")) : "Unpublished";

			return View(model);
		}

		/// <summary>
		/// Saves the given post model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The redirect result</returns>
		[HttpPost]
		[Route("post/save")]
		[ValidateInput(false)]
		[ValidateAntiForgeryToken]
		public ActionResult Save(EditModel model) {
			if (ModelState.IsValid) {
				model.Save(api);
				IsSaved = true;
				return RedirectToAction("Edit", new { id = model.Id });
			}
			if (model.Id.HasValue)
				ViewBag.Title = Piranha.Manager.Resources.Post.EditTitle;
			else ViewBag.Title = String.Format(Piranha.Manager.Resources.Post.AddTitle, model.TypeName);

			return View("Edit", model);
		}

		/// <summary>
		/// Deletes the post with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The redirect result</returns>
		[Route("post/delete/{id:Guid}")]
		public ActionResult Delete(Guid id) {
			api.Posts.Remove(id);
			api.SaveChanges();

			return RedirectToAction("List");
		}

		/// <summary>
		/// Changes the approval status for the specifed comment.
		/// </summary>
		/// <param name="state">The status</param>
		/// <returns>The partial comment list.</returns>
		[HttpPost]
		[Route("post/comment/approve")]
		public ActionResult CommentApprove(CommentState state) {
			var comment = api.Comments.GetSingle(where: c => c.Id == state.CommentId && c.PostId == state.PostId);

			if (comment != null) {
				comment.IsApproved = state.Status;
				api.SaveChanges();
			}
			return View("Partial/CommentList", api.Comments.Get(where: c => c.PostId == state.PostId)
				.Select(c => new Piranha.Manager.Models.Post.EditModel.CommentListItem() {
					Author = c.Author,
					Body = c.Body,
					Created = c.Created,
					Email = c.Email,
					Id = c.Id,
					IsApproved = c.IsApproved,
					IsSpam = c.IsSpam,
					WebSite = c.WebSite
				}).ToList());
		}

		/// <summary>
		/// Changes the spam status for the specifed comment.
		/// </summary>
		/// <param name="state">The status</param>
		/// <returns>The partial comment list.</returns>
		[HttpPost]
		[Route("post/comment/spam")]
		public ActionResult CommentSpam(CommentState state) {
			var comment = api.Comments.GetSingle(where: c => c.Id == state.CommentId && c.PostId == state.PostId);

			if (comment != null) {
				comment.IsSpam = state.Status;
				api.SaveChanges();
			}
			return View("Partial/CommentList", api.Comments.Get(where: c => c.PostId == state.PostId)
				.Select(c => new Piranha.Manager.Models.Post.EditModel.CommentListItem() {
					Author = c.Author,
					Body = c.Body,
					Created = c.Created,
					Email = c.Email,
					Id = c.Id,
					IsApproved = c.IsApproved,
					IsSpam = c.IsSpam,
					WebSite = c.WebSite
				}).ToList());
		}
	}
}