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

namespace Piranha.Log
{
	/// <summary>
	/// Interface for creating a log provider.
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// Writes the given message to the log.
		/// </summary>
		/// <param name="level">The level</param>
		/// <param name="message">The log message</param>
		/// <param name="exception">The optional exception</param>
		void Log(LogLevel level, string message, Exception exception = null);
	}
}
