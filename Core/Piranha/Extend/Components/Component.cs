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

namespace Piranha.Extend.Components
{
	/// <summary>
	/// Base class for creating a simple extension.
	/// </summary>
	/// <typeparam name="T">The value type</typeparam>
	public abstract class Component<T> : IComponent
	{
		#region Members
		/// <summary>
		/// The private value formatter.
		/// </summary>
		private readonly Func<T, object> FormatValue;
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the value.
		/// </summary>
		public T Value { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="formatter">The value formatter</param>
		public Component(Func<T, object> formatter) {
			FormatValue = formatter;
		}

		/// <summary>
		/// Transforms the extensions value for the client models.
		/// </summary>
		/// <returns>The transformed value</returns>
		public object GetValue() {
			return FormatValue(Value);
		}
	}
}
