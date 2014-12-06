/*
 * Copyright (c) 2014 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Piranha.AspNet
{
	public static class Hooks
	{
		/// <summary>
		/// The different hooks available for the ASP.NET
		/// runtime environment.
		/// </summary>
		public static class Delegates
		{
			/// <summary>
			/// Delegate for adding an assembly to the precompiled view engine.
			/// </summary>
			/// <param name="assemblies"></param>
			public delegate void PrecompiledViewEngingeRegistration(IList<Assembly> assemblies);
		}

		/// <summary>
		/// Called when the view engines are registered. This hooks can be used
		/// to add an assembly to be registered for precompiled views.
		/// </summary>
		public static Delegates.PrecompiledViewEngingeRegistration RegisterPrecompiledViews;
	}
}
