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
	/// The model hooks available
	/// </summary>
	public static class Models
	{
		/// <summary>
		/// The different delegates used by the model hooks.
		/// </summary>
		public static class Delegates
		{
			/// <summary>
			/// Delegate for modifying a model.
			/// </summary>
			/// <typeparam name="T">The model type</typeparam>
			/// <param name="model">The model</param>
			public delegate void ModelDelegate<T>(T model);
		}

		/// <summary>
		/// The model hooks available for blocks.
		/// </summary>
		public static class Block
		{
			/// <summary>
			/// Called when the block is loaded from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Block> OnLoad;

			/// <summary>
			/// Called when the block is saved to the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Block> OnSave;

			/// <summary>
			/// Called when the block is deleted from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Block> OnDelete;
		}

		/// <summary>
		/// The model hooks available for comments.
		/// </summary>
		public static class Comment
		{
			/// <summary>
			/// Called when the comment is loaded from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Comment> OnLoad;

			/// <summary>
			/// Called when the comment is saved to the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Comment> OnSave;

			/// <summary>
			/// Called when the comment is deleted from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Comment> OnDelete;
		}

		/// <summary>
		/// The model hooks available for pages.
		/// </summary>
		public static class Page
		{
			/// <summary>
			/// Called when the page is loaded from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Page> OnLoad;

			/// <summary>
			/// Called when the page is saved to the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Page> OnSave;

			/// <summary>
			/// Called when the page is deleted from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Page> OnDelete;
		}

		/// <summary>
		/// The model hooks available for posts.
		/// </summary>
		public static class Post
		{
			/// <summary>
			/// Called when the post is loaded from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Post> OnLoad;

			/// <summary>
			/// Called when the post is saved to the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Post> OnSave;

			/// <summary>
			/// Called when the post is deleted from the database.
			/// </summary>
			public static Delegates.ModelDelegate<Piranha.Models.Post> OnDelete;
		}
	}
}
