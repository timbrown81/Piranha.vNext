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
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Abstract base class for all manager controllers.
	/// </summary>
	public abstract class ManagerController : Controller
	{
		#region Properties
		/// <summary>
		/// The api.
		/// </summary>
		protected readonly Api api = new Api();

		/// <summary>
		/// Gets/sets if the current operation is a successfuly
		/// save operation.
		/// </summary>
		protected bool IsSaved {
			get { return ViewBag.IsSaved == true; }
			set {
				ViewBag.IsSaved = value;
				TempData["IsSaved"] = value;
			}
		}
		#endregion

		/// <summary>
		/// Returns a Json object with the given status code and rendered view.
		/// </summary>
		/// <param name="success">The status code</param>
		/// <param name="result">The view</param>
		/// <returns>The result</returns>
		protected ActionResult JsonView(bool success, ViewResult result) {
			return Json(new {
				success = success,
				body = result != null ? ViewToString(result) : null
			});
		}

		/// <summary>
		/// Returns a Json object with the given status code and data.
		/// </summary>
		/// <param name="success">The status code</param>
		/// <param name="data">The data</param>
		/// <returns>The result</returns>
		protected ActionResult JsonView(bool success, object data = null) {
			return Json(new {
				success = success,
				body = data
			});
		}

		/// <summary>
		/// Returns a Json object with the given status code and data.
		/// </summary>
		/// <param name="success">The status code</param>
		/// <param name="data">The data</param>
		/// <returns>The result</returns>
		protected ActionResult JsonData(bool success, object data = null) {
			return Json(new {
				success = success,
				data = data
			}, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Renders the given view result to a string
		/// </summary>
		/// <param name="viewResult">The view result</param>
		/// <returns>A html string</returns>
		protected string ViewToString(ViewResult viewResult) {
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			//Finding rendered view
			var view = ViewEngines.Engines.FindView(ControllerContext, viewResult.ViewName, viewResult.MasterName).View;

			//Creating view context
			var viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, sw);
			view.Render(viewContext, sw);
			return sb.ToString();
		}

		/// <summary>
		/// Do additional security checks for the manager area.
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			ViewBag.IsSaved = TempData["IsSaved"];
			TempData["IsSaved"] = false;
		}

		/// <summary>
		/// Disposes the controller
		/// </summary>
		/// <param name="disposing">State of disposal</param>
		protected override void Dispose(bool disposing) {
			// Dispose the api
			api.Dispose();

			// Dispose the base controller
			base.Dispose(disposing);
		}
	}
}