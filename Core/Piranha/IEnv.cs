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

namespace Piranha
{
	/// <summary>
	/// Interface defining a runtime environment.
	/// </summary>
	public interface IEnv
	{
		/// <summary>
		/// Gets/sets the current item being processed.
		/// </summary>
		Client.Models.Current GetCurrent();

		/// <summary>
		/// Gets/sets the current item being processed.
		/// </summary>
		void SetCurrent(Client.Models.Current current);

		/// <summary>
		/// Generates an absolute url from the given virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>The absolute url</returns>
		string AbsoluteUrl(string virtualpath);

		/// <summary>
		/// Generates an url from the given virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>The url</returns>
		string Url(string virtualpath);
	}
}
