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
using System.Drawing;
using System.IO;
using System.Linq;

namespace Piranha.Server.Handlers
{
	/// <summary>
	/// Handler for media files.
	/// </summary>
	public class MediaHandler : IHandler
	{
		/// <summary>
		/// Tries to handle an incoming request.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="request">The incoming route request</param>
		/// <returns>The result</returns>
		public IResponse Handle(Api api, IRequest request) {
			var slug = request.Segments.Length > 1 ? request.Segments[1] : "";
			int? width = null, height = null;

			if (!String.IsNullOrWhiteSpace(slug)) {
				var index = slug.LastIndexOf('.');

				if (index != -1) {
					var name = slug.Substring(0, index);
					var ending = slug.Substring(index);

					var segments = name.Split(new char[] { '_' });

					if (segments.Length > 2) {
						height = Convert.ToInt32(segments[2]);
					}
					if (segments.Length > 1) {
						width = Convert.ToInt32(segments[1]);
					}
					slug = segments[0] + ending;
				}

				var media = api.Media.GetSingle(slug);

				if (media != null) {
					var response = request.StreamResponse();
					var data = App.Media.Get(media);

					if (data != null) {
						response.ContentType = media.ContentType;

						if (width.HasValue) {
							using (var mem = new MemoryStream(data)) {
								// Get the image
								var image = Image.FromStream(mem);								

								// Scale & resize
								image = height.HasValue ? 
									Drawing.ImageUtils.Resize(image, width.Value, height.Value) :
									Drawing.ImageUtils.Resize(image, width.Value);

								// Save to output stream
								image.Save(response.OutputStream, image.RawFormat);
							}
						} else {
							using (var writer = new BinaryWriter(response.OutputStream)) {
								writer.Write(data);
							}
						}
						return response;
					}
				}
			}
			return null;
		}
	}
}
