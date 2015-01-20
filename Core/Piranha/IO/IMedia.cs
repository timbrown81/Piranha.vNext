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
	/// Interface for creating an media provider.
	/// </summary>
	public interface IMedia
	{
		/// <summary>
		/// Gets the binary data for the given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		/// <returns>The binary data</returns>
		byte[] Get(Models.Media media);

		/// <summary>
		/// Saves the given binary data for the given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		/// <param name="bytes">The binary data</param>
		void Put(Models.Media media, byte[] bytes);

		/// <summary>
		/// Saves the binary data available in the stream in the
		/// given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		/// <param name="stream">The stream</param>
		void Put(Models.Media media, Stream stream);

		/// <summary>
		/// Deletes the binary data for the given media object.
		/// </summary>
		/// <param name="media">The media object</param>
		void Delete(Models.Media media);
	}
}
