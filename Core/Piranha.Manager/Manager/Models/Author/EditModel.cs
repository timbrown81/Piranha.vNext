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

namespace Piranha.Manager.Models.Author
{
	/// <summary>
	/// View model for the author edit view.
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
		/// Gets/sets the email address.
		/// </summary>
		[StringLength(128)]
		public string Email { get; set; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		[StringLength(512)]
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the gravatar url.
		/// </summary>
		public string GravatarUrl { get; set; }
		#endregion

		/// <summary>
		/// Gets the edit model for the author with the given id.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="id">The unique id</param>
		/// <returns>The edit model</returns>
		public static EditModel GetById(Api api, Guid id) {
			var author = api.Authors.GetSingle(id);

			if (author != null) {
				var model = Mapper.Map<Piranha.Models.Author, EditModel>(author);
				var ui = new Client.Helpers.UIHelper();

				model.GravatarUrl = ui.GravatarUrl(model.Email, 80);

				return model;
			}
			return null;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		public void Save(Api api) {
			var newModel = false;

			// Get or create author
			var author = Id.HasValue ? api.Authors.GetSingle(Id.Value) : null;
			if (author == null) {
				author = new Piranha.Models.Author();
				newModel = true;
			}

			// Map values
			Mapper.Map<EditModel, Piranha.Models.Author>(this, author);

			if (newModel)
				api.Authors.Add(author);
			api.SaveChanges();

			this.Id = author.Id;
		}
	}
}
