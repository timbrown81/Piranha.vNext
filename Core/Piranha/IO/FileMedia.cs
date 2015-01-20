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

namespace Piranha.IO
{
	/// <summary>
	/// Media provider for storing media to file.
	/// </summary>
	public class FileMedia : IMedia
	{
		#region Members
		private readonly string mediaPath = Path.Combine("App_Data", Path.Combine("Uploads", "Media"));
		private readonly string cachePath = Path.Combine("App_Data", Path.Combine("Cache", "Media"));
		private readonly string mediaMapped;
		private readonly string cacheMapped;
		private readonly bool disabled;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public FileMedia() {
			if (AppDomain.CurrentDomain != null && !String.IsNullOrEmpty(AppDomain.CurrentDomain.BaseDirectory)) {
				mediaMapped = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, mediaPath);
				cacheMapped = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cachePath);

				// Ensure directories
				if (!Directory.Exists(mediaMapped))
					Directory.CreateDirectory(mediaMapped);
				if (!Directory.Exists(cacheMapped))
					Directory.CreateDirectory(cacheMapped);
			} else {
				disabled = true;
			}
		}

		/// <summary>
		/// Gets the binary data for the given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		/// <returns>The binary data</returns>
		public byte[] Get(Models.Media media) {
			if (!disabled) {
				var path = Path.Combine(mediaMapped, media.Id.ToString());

				if (File.Exists(path)) {
					return File.ReadAllBytes(path);
				}
			}
			return null;
		}

		/// <summary>
		/// Saves the given binary data for the given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		/// <param name="bytes">The binary data</param>
		public void Put(Models.Media media, byte[] bytes) {
			if (!disabled) {
				using (var writer = new FileStream(Path.Combine(mediaMapped, media.Id.ToString()), FileMode.Create)) {
					writer.Write(bytes, 0, bytes.Length);
				}
			}
		}

		/// <summary>
		/// Saves the binary data available in the stream in the
		/// given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		/// <param name="stream">The stream</param>
		public async void Put(Models.Media media, Stream stream) {
			if (!disabled) {
				using (var writer = new FileStream(Path.Combine(mediaMapped, media.Id.ToString()), FileMode.Create)) {
					await stream.CopyToAsync(writer);
				}
			}
		}

		/// <summary>
		/// Deletes the binary data for the given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		public void Delete(Models.Media media) {
			File.Delete(Path.Combine(mediaMapped, media.Id.ToString()));
		}
	}
}
