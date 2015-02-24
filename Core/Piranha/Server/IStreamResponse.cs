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

namespace Piranha.Server
{
	/// <summary>
	/// Interface for a stream response.
	/// </summary>
	public interface IStreamResponse : IResponse
	{
		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		string ContentType { get; set; }

		/// <summary>
		/// Gets/sets the content encoding.
		/// </summary>
		Encoding ContentEncoding { get; set; }

		/// <summary>
		/// Gets the output stream.
		/// </summary>
		Stream OutputStream { get; }
	}
}
