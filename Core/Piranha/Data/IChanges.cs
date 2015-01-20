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
	/// Base interface for all models that keep track on when they're changed.
	/// </summary>
	public interface IChanges
	{
		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		DateTime Updated { get; set; }
	}
}
