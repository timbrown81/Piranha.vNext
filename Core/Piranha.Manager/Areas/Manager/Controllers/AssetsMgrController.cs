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