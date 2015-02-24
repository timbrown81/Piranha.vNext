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

namespace Piranha.Models.Base
{
	/// <summary>
	/// Base class for content.
	/// </summary>
	/// <typeparam name="TType">The content type</typeparam>
	public abstract class Content<TType> : Model, Data.IModel, Data.IChanges, Data.IPublishable
		where TType : ContentType
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the id of the content type.
		/// </summary>
		public abstract Guid TypeId { get; set; }

		/// <summary>
		/// Gets/sets the author responsible for this content.
		/// </summary>
		public Guid AuthorId { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		public abstract string Slug { get; set; }

		/// <summary>
		/// Gets/sets the optional keywords.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the optional route that should handle requests.
		/// </summary>
		public string Route { get; set; }

		/// <summary>
		/// Gets/sets the optional view that should render requests.
		/// </summary>
		public string View { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially published.
		/// </summary>
		public DateTime? Published { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the author responsible for this content.
		/// </summary>
		public Author Author { get; set; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public TType Type { get; set; }
		#endregion
	}
}
