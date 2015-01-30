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
using Piranha.Client.Models;
using Piranha.Models;

namespace Piranha.Server.Handlers
{
	public class ArchiveHandler : IHandler
	{
		/// <summary>
		/// Handles the request and rewrites it to the appropriate route.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="request">The current request</param>
		/// <returns>The response</returns>
		public IResponse Handle(Api api, IRequest request) {
			if (request.Segments.Length > 0) {
				ArchiveType? type = null;

				// 1: Try to find archive type by slug
				if (request.Segments[0] == Config.Permalinks.PostArchiveSlug) {
					type = ArchiveType.Post;
				} else if (request.Segments[0] == Config.Permalinks.CategoryArchiveSlug) {
					type = ArchiveType.Category;
				} else if (request.Segments[0] == Config.Permalinks.TagArchiveSlug) {
					type = ArchiveType.Tag;
				}

				if (type.HasValue) {
					IArchived archive = null;
					var offset = type == ArchiveType.Post ? 1 : 2;

					if (request.Segments.Length >= offset) {
						var slug = offset > 1 ? request.Segments[1] : null;
						Guid? id = null;

						if (type == ArchiveType.Post) {
							archive = PostArchive.Instance;
						} else if (type == ArchiveType.Category) {
							var category = api.Categories.GetSingle(where: c => c.Slug == slug);
							if (category != null)
								id = category.Id;
							archive = category;
						}

						if (type == ArchiveType.Post || id.HasValue) {
							int? page = null;
							int? year = null;
							int? month = null;
							bool foundPage = false;

							for (var n = offset; n < request.Segments.Length; n++) {
								if (request.Segments[n] == "page") {
									foundPage = true;
									continue;
								}

								if (foundPage) {
									try {
										page = Convert.ToInt32(request.Segments[n]);

									} catch { }
									break;
								}

								if (!year.HasValue) {
									try {
										year = Convert.ToInt32(request.Segments[n]);

										if (year.Value > DateTime.Now.Year)
											year = DateTime.Now.Year;
									} catch { }
								} else {
									try {
										month = Math.Max(Math.Min(Convert.ToInt32(request.Segments[n]), 12), 1);
									} catch { }
								}
							}

							// Set current
							App.Env.SetCurrent(new Current() {
								Id = id.HasValue ? id.Value : Guid.Empty,
								Title = archive.ArchiveTitle,
								Keywords = archive.MetaKeywords,
								Description = archive.MetaDescription,
								VirtualPath = "~/" + archive.ArchiveSlug + (year.HasValue ? "/" + year + (month.HasValue ? "/" + month : "") : ""),
								Type = CurrentType.Archive
							});

							var response = request.RewriteResponse();

							response.Route = "archive"; // TODO: Fix
							response.Params = request.Params.Concat(new Param[] { 
								new Param() { Key = "id", Value = id.ToString() },
								new Param() { Key = "type", Value = type.ToString() },
								new Param() { Key = "year", Value = year.ToString() },
								new Param() { Key = "month", Value = month.ToString() },
								new Param() { Key = "page", Value = page.ToString() }
							}).ToArray();

							return response;
						}
					}
				}
			}
			return null;
		}
	}
}
