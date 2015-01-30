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
using System.Collections.Generic;
using System.Web.Mvc;
using Piranha.Manager.Models.Config;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for managing application & module config.
	/// </summary>
	[Authorize]
	[RouteArea("Manager", AreaPrefix = "manager")]
	public class ConfigMgrController : ManagerController
	{
		/// <summary>
		/// Gets the main view for the config.
		/// </summary>
		/// <returns>The list view</returns>
		[Route("config")]
		public ActionResult List() {
			Piranha.Manager.Config.Refresh(api);

			return View(Piranha.Manager.Config.Blocks);
		}

		/// <summary>
		/// Gets the list model data.
		/// </summary>
		/// <returns>The model data</returns>
		[Route("config/get")]
		public ActionResult Get() {
			return JsonData(true, EditModel.Get(api));
		}

		/// <summary>
		/// Saves the given site configuration.
		/// </summary>
		/// <param name="model">The config model</param>
		/// <returns>The result and updated config</returns>
		[HttpPost]
		[Route("config/site/save")]
		public ActionResult SaveSite(EditModel.SiteModel model) {
			Piranha.Config.Site.Title = model.Title;
			Piranha.Config.Site.Tagline = model.Tagline;
			Piranha.Config.Site.Description = model.Description;

			return JsonData(true, EditModel.Get(api));
		}

		/// <summary>
		/// Saves the given archive configuration.
		/// </summary>
		/// <param name="model">The archive model</param>
		/// <returns>The result and updated config</returns>
		[HttpPost]
		[Route("config/archive/save")]
		public ActionResult SaveSite(EditModel.ArchiveModel model) {
			Piranha.Config.Site.ArchiveTitle = model.Title;
			Piranha.Config.Site.ArchivePageSize = model.PageSize;
			Piranha.Config.Site.ArchiveKeywords = model.Keywords;
			Piranha.Config.Site.ArchiveDescription = model.Description;

			return JsonData(true, EditModel.Get(api));
		}

		/// <summary>
		/// Saves the given permalink configuration.
		/// </summary>
		/// <param name="model">The permalink model</param>
		/// <returns>The result and updated config</returns>
		[HttpPost]
		[Route("config/permalinks/save")]
		public ActionResult SaveSite(EditModel.PermalinkModel model) {
			Piranha.Config.Permalinks.PageSlug = model.PageSlug;
			Piranha.Config.Permalinks.PostSlug = model.PostSlug;
			Piranha.Config.Permalinks.PostArchiveSlug = model.PostArchiveSlug;
			Piranha.Config.Permalinks.CategoryArchiveSlug = model.CategoryArchiveSlug;
			Piranha.Config.Permalinks.TagArchiveSlug = model.TagArchiveSlug;

			return JsonData(true, EditModel.Get(api));
		}

		/// <summary>
		/// Saves the given cache configuration.
		/// </summary>
		/// <param name="model">The config model</param>
		/// <returns>The result and updated config</returns>
		[HttpPost]
		[Route("config/cache/save")]
		public ActionResult SaveCache(EditModel.CacheModel model) {
			Piranha.Config.Cache.Expires = model.Expires;
			Piranha.Config.Cache.MaxAge = model.MaxAge;

			return JsonData(true, EditModel.Get(api));
		}

		/// <summary>
		/// Saves the given comment configuration.
		/// </summary>
		/// <param name="model">The config model</param>
		/// <returns>The result and updated config</returns>
		[HttpPost]
		[Route("config/comments/save")]
		public ActionResult SaveComments(EditModel.CommentModel model) {
			Piranha.Config.Comments.ModerateAnonymous = model.ModerateAnonymous;
			Piranha.Config.Comments.ModerateAuthorized = model.ModerateAuthorized;
			Piranha.Config.Comments.NotifyAuthor = model.NotifyAuthor;
			Piranha.Config.Comments.NotifyModerators = model.NotifyModerators;
			Piranha.Config.Comments.Moderators = model.Moderators;

			return JsonData(true, EditModel.Get(api));
		}

		/// <summary>
		/// Saves the given dynamic config block.
		/// </summary>
		/// <param name="block">The config block</param>
		/// <returns>The result and updated config</returns>
		[HttpPost]
		[Route("config/block/save")]
		public ActionResult SaveBlock(IList<EditModel.ParamModel> block) {
			foreach (var item in block) {
				var param = api.Params.GetSingle(where: p => p.Name == item.Name);
				if (param != null)
					param.Value = item.Value;
			}
			api.SaveChanges();

			return JsonData(true, EditModel.Get(api));
		}
	}
}