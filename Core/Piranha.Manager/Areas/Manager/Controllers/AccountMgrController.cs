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
using System.Web.Mvc;
using Piranha.Manager.Models.Account;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Manager controller for authentication.
	/// </summary>
	[RouteArea("Manager", AreaPrefix = "manager")]
	public class AccountMgrController : Controller
	{
		/// <summary>
		/// Get the login view.
		/// </summary>
		/// <returns>The view result</returns>
		[HttpGet]
		[Route("login")]
		public ActionResult Login() {
			return View();
		}

		/// <summary>
		/// Signs in the user with the given credentials.
		/// </summary>
		/// <param name="m">The login model</param>
		/// <returns>The redirect result</returns>
		[HttpPost]
		[Route("login")]
		[ValidateAntiForgeryToken]
		public ActionResult LoginUser(LoginModel m) {
			if (ModelState.IsValid) {
				if (App.Security.SignIn(m.Username, m.Password))
					return RedirectToAction("List", "PostMgr");
			}
			return RedirectToAction("Login");
		}

		/// <summary>
		/// Signs out the currently authenticated user.
		/// </summary>
		/// <returns>The redirect result</returns>
		[HttpGet]
		[Route("logout")]
		public ActionResult LogoutUser() {
			App.Security.SignOut();
			return RedirectToAction("Login");
		}
	}
}