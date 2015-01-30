/*
 * Copyright (c) 2014 Håkan Edling
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
using Piranha.Client.Models;

namespace Piranha.AspNet.Mvc
{
	/// <summary>
	/// Base controller for a post archive.
	/// </summary>
	public abstract class ArchiveController : Controller
	{
		#region Properties
		/// <summary>
		/// Gets the currently requested archive type.
		/// </summary>
		protected ArchiveType Type { get; private set; }

		/// <summary>
		/// Gets the optional id of the category type data.
		/// </summary>
		protected Guid? Id { get; private set; }

		/// <summary>
		/// Gets the currently requested year.
		/// </summary>
		protected int? Year { get; private set; }

		/// <summary>
		/// Gets the currently requested month.
		/// </summary>
		protected int? Month { get; private set; }

		/// <summary>
		/// Gets the currently requested page.
		/// </summary>
		protected int? Page { get; private set; }
		#endregion

		/// <summary>
		/// Gets the archive model for the current request.
		/// </summary>
		/// <returns>The model</returns>
		protected ArchiveModel GetModel() {
			return GetModel<ArchiveModel>();
		}

		/// <summary>
		/// Gets the archive model for the current request.
		/// </summary>
		/// <returns>The model</returns>
		protected ArchiveModel GetModel<T>() where T : ArchiveModel {
			if (Type == ArchiveType.Category) {
				return ArchiveModel.GetCategoryArchive<T>(Id.Value, Page, Year, Month);
			} else if (Type == ArchiveType.Post) {
				return ArchiveModel.GetPostArchive<T>(Page, Year, Month);
			}
			return null;
		}

		/// <summary>
		/// Initializes the controller.
		/// </summary>
		/// <param name="filterContext">The current filter context</param>
		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			if (App.Env.GetCurrent() != null) {
				Id = !String.IsNullOrWhiteSpace(Request["id"]) ? (Guid?)new Guid(Request["id"]) : null;
				Type = (ArchiveType)Enum.Parse(typeof(ArchiveType), Request["type"]);

				try {
					Year = Convert.ToInt32(Request["year"]);
				} catch { }

				if (Year.HasValue) {
					try {
						Month = Convert.ToInt32(Request["month"]);
					} catch { }
				}

				try {
					Page = Convert.ToInt32(Request["page"]);
				} catch { }
	
				base.OnActionExecuting(filterContext);
			} else {
				throw new System.Web.HttpException(403, "No direct access");
			}
		}
	}
}
