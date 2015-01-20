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

namespace Piranha.Hooks
{
	/// <summary>
	/// The main application hooks available.
	/// </summary>
	public static class App
	{
		/// <summary>
		/// The different delegates used by the app hooks.
		/// </summary>
		public static class Delegates
		{
			/// <summary>
			/// Delegate for request hooks.
			/// </summary>
			/// <param name="context">The current http context</param>
			public delegate void RequestDelegate(Server.IRequest request);

			/// <summary>
			/// Delegate for request errors.
			/// </summary>
			/// <param name="context">The current http context</param>
			public delegate void ErrorDelegate(Server.IRequest context, Exception e);
		}

		/// <summary>
		/// The hooks available for the http request.
		/// </summary>
		public static class Request
		{
			/// <summary>
			/// Called when the request begins before the Piranha module.
			/// </summary>
			public static Delegates.RequestDelegate OnBeginRequest;

			/// <summary>
			/// Called when the request ends after the Piranha module.
			/// </summary>
			public static Delegates.RequestDelegate OnEndRequest;

			/// <summary>
			/// Called when an unhandled exception occurs in an application request.
			/// </summary>
			public static Delegates.ErrorDelegate OnError;
		}
	}
}
