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

namespace Piranha.Manager.Models.Block
{
	/// <summary>
	/// View model for the block edit view.
	/// </summary>
	public sealed class EditModel
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		[Required, StringLength(128)]
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		[StringLength(128)]
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		[StringLength(255)]
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the main body.
		/// </summary>
		public string Body { get; set; }
		#endregion

		/// <summary>
		/// Gets the edit model for the block with the given id.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="id">The unique id</param>
		/// <returns>The edit model</returns>
		public static EditModel GetById(Api api, Guid id) {
			var block = api.Blocks.GetSingle(id);

			if (block != null)
				return Mapper.Map<Piranha.Models.Block, EditModel>(block);
			return new EditModel();
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		public void Save(Api api) {
			bool newModel = false;

			// Get or create block
			var block = Id.HasValue ? api.Blocks.GetSingle(Id.Value) : null;
			if (block == null) {
				block = new Piranha.Models.Block();
				newModel = true;
			}

			// Map values
			Mapper.Map<EditModel, Piranha.Models.Block>(this, block);

			if (newModel)
				api.Blocks.Add(block);
			api.SaveChanges();

			this.Id = block.Id;
		}
	}
}
