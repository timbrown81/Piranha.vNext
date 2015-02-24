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
	/// Base interface for all models that can be published.
	/// </summary>
	public interface IPublishable
	{
		/// <summary>
		/// Gets/sets the publish date.
		/// </summary>
		DateTime? Published { get; set; }
	}
}
