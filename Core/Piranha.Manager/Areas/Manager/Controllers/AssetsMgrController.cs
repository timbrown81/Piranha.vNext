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
using Piranha.Manager;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for handling embedded resources.
	/// </summary>
	[RouteArea("Manager", AreaPrefix = "manager")]
	public class AssetsMgrController : Controller
	{
		/// <summary>
		/// Gets an embedded resource.
		/// </summary>
		/// <param name="path">The resource path</param>
		/// <returns>The resource</returns>
		[Route("assets.ashx/{*path}")]
		public ActionResult GetAsset(string path) {
			var resource = path.Replace("/", ".").ToLower();

			if (!AspNet.Cache.BrowserCache.IsCached(HttpContext, resource, ManagerModule.LastModified)) {
				if (ManagerModule.Resources.ContainsKey(resource)) {
					var res = ManagerModule.Resources[resource] ;

					using (var stream = typeof(AssetsMgrController).Assembly.GetManifestResourceStream(res.Name)) {
						var bytes = new byte[stream.Length] ;
						stream.Read(bytes, 0, Convert.ToInt32(stream.Length)) ;

						return File(bytes, res.ContentType);
					}
				} else {
					return HttpNotFound();
				}
			}
			return null;
		}
	}
}