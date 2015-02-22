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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Piranha.Extend
{
	/// <summary>
	/// The extension manager is responsible for managing all
	/// composable parts of the application.
	/// </summary>
	public sealed class ExtensionManager
	{
		#region Inner classes
		/// <summary>
		/// An imported component.
		/// </summary>
		public sealed class Import
		{
			/// <summary>
			/// Gets the display name.
			/// </summary>
			public string Name { get; internal set; }

			/// <summary>
			/// Gets the CLR value type.
			/// </summary>
			public Type ValueType { get; internal set; }

			/// <summary>
			/// Gets the component type.
			/// </summary>
			public ComponentType Type { get; internal set; }

			/// <summary>
			/// Internal constructor.
			/// </summary>
			internal Import() { }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/sets the available components.
		/// </summary>
		private IList<Import> Components { get; set; }

		/// <summary>
		/// Gets the currently imported modules.
		/// </summary>
		public IList<IModule> Modules { get; private set; }

		/// <summary>
		/// Gets the available content blocks.
		/// </summary>
		public IList<Import> ContentBlocks {
			get { return Components.Where(i => i.Type.HasFlag(ComponentType.ContentBlock)).OrderBy(i => i.Name).ToList(); }
		}

		/// <summary>
		/// Gets the available template fields.
		/// </summary>
		public IList<Import> TemplateFields {
			get { return Components.Where(i => i.Type.HasFlag(ComponentType.TemplateField)).OrderBy(i => i.Name).ToList(); }
		}
		#endregion

		/// <summary>
		/// Default internal constructor.
		/// </summary>
		internal ExtensionManager() {
			Components = new List<Import>();
			Modules = new List<IModule>();

			// Scan all assemblies
			App.Logger.Log(Log.LogLevel.INFO, "ExtensionManager: Scanning assemblies");
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				foreach (var info in assembly.DefinedTypes) {
					if (info.IsClass && !info.IsAbstract) {
						if (typeof(IComponent).GetTypeInfo().IsAssignableFrom(info)) {
							//
							// Components
							//
							var attr = info.GetCustomAttribute<ComponentAttribute>();
							if (attr != null) {
								Components.Add(new Import() {
									Name = attr.Name,
									Type = attr.Type,
									ValueType = info
								});
							}
						} else if (typeof(IModule).GetTypeInfo().IsAssignableFrom(info)) {
							//
							// Modules
							//
							Modules.Add((IModule)Activator.CreateInstance(info));
						} 
					} 
				}
			}

			// Initialize all modules
			App.Logger.Log(Log.LogLevel.INFO, "ExtensionManager: Initializing modules");
			foreach (var module in Modules)
				module.Init();
		}
	}
}
