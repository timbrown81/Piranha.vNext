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
using System.ComponentModel.DataAnnotations;

namespace Piranha.Manager.Models.Alias
{
	/// <summary>
	/// View model for the alias edit view.
	/// </summary>
	public sealed class EditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// Gets/sets the old URL.
		/// </summary>
		[Required, StringLength(255)]
		public string OldUrl { get; set; }

		/// <summary>
		/// Gets/sets the new URL.
		/// </summary>
		[Required, StringLength(255)]
		public string NewUrl { get; set; }

		/// <summary>
		/// Gets/sets if this is a permanent redirect or not.
		/// </summary>
		public bool IsPermanent { get; set; }
		#endregion

		/// <summary>
		/// Gets the edit model for the alias with the given id.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="id">The unique id</param>
		/// <returns>The edit model</returns>
		public static EditModel GetById(Api api, Guid id) {
			var alias = api.Aliases.GetSingle(id);

			if (alias != null)
				return Mapper.Map<Piranha.Models.Alias, EditModel>(alias);
			return null;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		public void Save(Api api) {
			var newModel = false;

			// Get or create alias
			var alias = Id.HasValue ? api.Aliases.GetSingle(Id.Value) : null;
			if (alias == null) {
				alias = new Piranha.Models.Alias();
				newModel = true;
			}

			// Map values
			Mapper.Map<EditModel, Piranha.Models.Alias>(this, alias);

			if (newModel)
				api.Aliases.Add(alias);
			api.SaveChanges();

			this.Id = alias.Id;
		}
	}
}
