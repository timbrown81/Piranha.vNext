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

namespace Piranha.Data
{
	/// <summary>
	/// Tools for seeding the database with default data.
	/// </summary>
	internal static class Seed
	{
		#region Members
		private const string ROLE_SYSADMIN = "Piranha SysAdmin";
		private const string ROLE_ADMIN = "Piranha Admin";
		#endregion

		/// <summary>
		/// Seed params.
		/// </summary>
		/// <param name="api">The current api</param>
		public static void Params(Api api) {
			var param = api.Params.GetSingle(where: p => p.Name == "site_title");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "site_title",
					Value = "Piranha CMS"
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "site_description");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "site_description",
					Value = "Welcome to Piranha CMS"
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "site_lastmodified");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "site_lastmodified",
					Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "archive_pagesize");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "archive_pagesize",
					Value = "10"
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "cache_expires");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "cache_expires",
					Value = "0"
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "cache_maxage");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "cache_maxage",
					Value = "0"
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "comment_moderate_authorized");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "comment_moderate_authorized",
					Value = false.ToString()
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "comment_moderate_anonymous");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "comment_moderate_anonymous",
					Value = false.ToString()
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "comment_notify_author");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "comment_notify_author",
					Value = false.ToString()
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "comment_notify_moderators");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "comment_notify_moderators",
					Value = false.ToString()
				});
			}

			param = api.Params.GetSingle(where: p => p.Name == "comment_moderators");
			if (param == null) {
				api.Params.Add(new Models.Param() {
					Name = "comment_moderators",
					Value = ""
				});
			}
			api.SaveChanges();
		}
	}
}
