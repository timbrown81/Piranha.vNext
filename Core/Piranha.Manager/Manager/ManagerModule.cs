/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using Piranha.Extend;

namespace Piranha.Manager
{
	/// <summary>
	/// The main entry point for the manager module.
	/// </summary>
	public class ManagerModule : IModule
	{
		#region Inner classes
		/// <summary>
		/// Class representing an embedded resource.
		/// </summary>
		internal class Resource
		{
			/// <summary>
			/// Gets/sets the resource name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets/sets the content type.
			/// </summary>
			public string ContentType { get; set; }
		}
		#endregion

		#region Members
		/// <summary>
		/// Gets the internal collection of embedded resources.
		/// </summary>
		internal static IDictionary<string, Resource> Resources { get; private set; }

		/// <summary>
		/// Gets the last modification date of the manager module.
		/// </summary>
		internal static DateTime LastModified { get; private set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ManagerModule() {
			Resources = new Dictionary<string, Resource>();
			LastModified = new FileInfo(typeof(ManagerModule).Assembly.Location).LastWriteTime;
		}

		/// <summary>
		/// Initializes the module. This method should be used for
		/// ensuring runtime resources and registering hooks.
		/// </summary>
		public void Init() {
			// Alias
			Mapper.CreateMap<Piranha.Models.Alias, Models.Alias.ListItem>()
				.ForMember(a => a.Saved, o => o.Ignore())
				.ForMember(a => a.Created, o => o.MapFrom(m => m.Created.ToString("yyyy-MM-dd")))
				.ForMember(a => a.Updated, o => o.MapFrom(m => m.Updated.ToString("yyyy-MM-dd")));
			Mapper.CreateMap<Piranha.Models.Alias, Models.Alias.EditModel>();
			Mapper.CreateMap<Models.Alias.EditModel, Piranha.Models.Alias>()
				.ForMember(a => a.Id, o => o.Ignore())
				.ForMember(a => a.Created, o => o.Ignore())
				.ForMember(a => a.Updated, o => o.Ignore());

			// Author
			Mapper.CreateMap<Piranha.Models.Author, Models.Author.ListItem>()
				.ForMember(a => a.Saved, o => o.Ignore())
				.ForMember(a => a.GravatarUrl, o => o.Ignore())
				.ForMember(a => a.Created, o => o.MapFrom(m => m.Created.ToString("yyyy-MM-dd")))
				.ForMember(a => a.Updated, o => o.MapFrom(m => m.Updated.ToString("yyyy-MM-dd")));
			Mapper.CreateMap<Piranha.Models.Author, Models.Author.EditModel>()
				.ForMember(a => a.GravatarUrl, o => o.Ignore());
			Mapper.CreateMap<Models.Author.EditModel, Piranha.Models.Author>()
				.ForMember(a => a.Id, o => o.Ignore())
				.ForMember(a => a.Created, o => o.Ignore())
				.ForMember(a => a.Updated, o => o.Ignore());

			// Block
			Mapper.CreateMap<Piranha.Models.Block, Models.Block.ListItem>()
				.ForMember(b => b.Saved, o => o.Ignore())
				.ForMember(b => b.Created, o => o.MapFrom(m => m.Created.ToString("yyyy-MM-dd")))
				.ForMember(b => b.Updated, o => o.MapFrom(m => m.Updated.ToString("yyyy-MM-dd")));
			Mapper.CreateMap<Piranha.Models.Block, Models.Block.EditModel>();
			Mapper.CreateMap<Models.Block.EditModel, Piranha.Models.Block>()
				.ForMember(b => b.Id, o => o.Ignore())
				.ForMember(b => b.Created, o => o.Ignore())
				.ForMember(b => b.Updated, o => o.Ignore());

			// Category
			Mapper.CreateMap<Piranha.Models.Category, Models.Category.ListItem>()
				.ForMember(c => c.Saved, o => o.Ignore())
				.ForMember(c => c.Created, o => o.MapFrom(m => m.Created.ToString("yyyy-MM-dd")))
				.ForMember(c => c.Updated, o => o.MapFrom(m => m.Updated.ToString("yyyy-MM-dd")));
			Mapper.CreateMap<Piranha.Models.Category, Models.Category.EditModel>();
			Mapper.CreateMap<Models.Category.EditModel, Piranha.Models.Category>()
				.ForMember(b => b.Id, o => o.Ignore())
				.ForMember(b => b.Created, o => o.Ignore())
				.ForMember(b => b.Updated, o => o.Ignore());

			Mapper.AssertConfigurationIsValid();

			// Register pre-compiled views
			AspNet.Hooks.RegisterPrecompiledViews += assemblies => {
				assemblies.Add(typeof(ManagerModule).Assembly);
			};

			// Scan precompiled resources
			foreach (var name in typeof(ManagerModule).Assembly.GetManifestResourceNames()) {
				Resources.Add(name.Replace("Piranha.Areas.Manager.Assets.", "").ToLower(), new Resource() {
					Name = name, ContentType = GetContentType(name)
				}) ;
			}
		}

		#region Private methods
		/// <summary>
		/// Gets the content type from the resource name.
		/// </summary>
		/// <param name="name">The resource name</param>
		/// <returns>The content type</returns>
		private string GetContentType(string name) {
			if (name.EndsWith(".js")) {
				return "text/javascript" ;
			} else if (name.EndsWith(".css")) {
				return "text/css" ;
			} else if (name.EndsWith(".png")) {
				return "image/png" ;
			} else if (name.EndsWith(".jpg")) {
				return "image/jpg" ;
			} else if (name.EndsWith(".gif")) {
				return "image/gif" ;
			} else if (name.EndsWith(".ico")) {
				return "image/ico" ;
			} else if (name.EndsWith(".eot")) {
				return "application/vnd.ms-fontobject" ;
			} else if (name.EndsWith(".ttf")) {
				return "application/octet-stream" ;
			} else if (name.EndsWith(".svg")) {
				return "image/svg+xml" ;
			} else if (name.EndsWith(".woff")) {
				return "application/x-woff" ;
			}
			return "application/unknown" ;
		}
		#endregion
	}
}