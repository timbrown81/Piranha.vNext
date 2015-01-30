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
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace Piranha.EntityFramework
{
	/// <summary>
	/// The internal db context.
	/// </summary>
	internal class Db : BaseContext<Db>
	{
		#region Db sets
		/// <summary>
		/// Gets/sets the alias set.
		/// </summary>
		public DbSet<Models.Alias> Aliases { get; set; }

		/// <summary>
		/// Gets/sets the author set.
		/// </summary>
		public DbSet<Models.Author> Authors { get; set; }

		/// <summary>
		/// Gets/sets the category set.
		/// </summary>
		public DbSet<Models.Category> Categories { get; set; }

		/// <summary>
		/// Gets/sets the comment set.
		/// </summary>
		public DbSet<Models.Comment> Comments { get; set; }

		/// <summary>
		/// Gets/sets the content set.
		/// </summary>
		public DbSet<Models.Content> Content { get; set; }

		/// <summary>
		/// Gets/sets the content block set.
		/// </summary>
		public DbSet<Models.ContentBlock> ContentBlocks { get; set; }

		/// <summary>
		/// Gets/sets the content row set.
		/// </summary>
		public DbSet<Models.ContentRow> ContentRows { get; set; }

		/// <summary>
		/// Gets/sets the block set.
		/// </summary>
		public DbSet<Models.Block> Blocks { get; set; }

		/// <summary>
		/// Gets/sets the media set.
		/// </summary>
		public DbSet<Models.Media> Media { get; set; }

		/// <summary>
		/// Gets/sets the param set.
		/// </summary>
		public DbSet<Models.Param> Params { get; set; }

		/// <summary>
		/// Gets/sets the ratings set.
		/// </summary>
		public DbSet<Models.Rating> Ratings { get; set; }

		/// <summary>
		/// Gets/sets the template set.
		/// </summary>
		public DbSet<Models.Template> Templates { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Db() : base("piranha", new MigrateDatabaseToLatestVersion<Db, Migrations.Configuration>()) {}

		/// <summary>
		/// Configures the data model.
		/// </summary>
		/// <param name="modelBuilder">The current model builder</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			// Alias
			modelBuilder.Entity<Models.Alias>().ToTable("PiranhaAliases");
			modelBuilder.Entity<Models.Alias>().Property(a => a.NewUrl).HasMaxLength(255).IsRequired();
			modelBuilder.Entity<Models.Alias>().Property(a => a.OldUrl).HasMaxLength(255).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));

			// Author
			modelBuilder.Entity<Models.Author>().ToTable("PiranhaAuthors");
			modelBuilder.Entity<Models.Author>().Property(a => a.Name).HasMaxLength(128).IsRequired();
			modelBuilder.Entity<Models.Author>().Property(a => a.Email).HasMaxLength(128);
			modelBuilder.Entity<Models.Author>().Property(a => a.Description).HasMaxLength(512);

			// Block
			modelBuilder.Entity<Models.Block>().ToTable("PiranhaBlocks");
			modelBuilder.Entity<Models.Block>().Property(b => b.Name).HasMaxLength(128).IsRequired();
			modelBuilder.Entity<Models.Block>().Property(b => b.Description).HasMaxLength(255);
			modelBuilder.Entity<Models.Block>().Property(b => b.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));
	
			// Category
			modelBuilder.Entity<Models.Category>().ToTable("PiranhaCategories");
			modelBuilder.Entity<Models.Category>().Property(c => c.Title).HasMaxLength(128).IsRequired();
			modelBuilder.Entity<Models.Category>().Property(c => c.ArchiveTitle).HasMaxLength(128);
			modelBuilder.Entity<Models.Category>().Property(c => c.MetaKeywords).HasMaxLength(128);
			modelBuilder.Entity<Models.Category>().Property(c => c.MetaDescription).HasMaxLength(255);
			modelBuilder.Entity<Models.Category>().Property(c => c.ArchiveView).HasMaxLength(255);
			modelBuilder.Entity<Models.Category>().Property(c => c.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));
			modelBuilder.Entity<Models.Category>().Ignore(c => c.ArchiveSlug);

			// Comment
			modelBuilder.Entity<Models.Comment>().ToTable("PiranhaComments");
			modelBuilder.Entity<Models.Comment>().Property(c => c.UserId).HasMaxLength(128);
			modelBuilder.Entity<Models.Comment>().Property(c => c.Author).HasMaxLength(128);
			modelBuilder.Entity<Models.Comment>().Property(c => c.Email).HasMaxLength(128);
			modelBuilder.Entity<Models.Comment>().Property(c => c.IP).HasMaxLength(16);
			modelBuilder.Entity<Models.Comment>().Property(c => c.UserAgent).HasMaxLength(128);
			modelBuilder.Entity<Models.Comment>().Property(c => c.WebSite).HasMaxLength(128);
			modelBuilder.Entity<Models.Comment>().Property(c => c.SessionID).HasMaxLength(64);

			// Content
			modelBuilder.Entity<Models.Content>().ToTable("PiranhaContent");
			modelBuilder.Entity<Models.Content>().Property(c => c.Title).HasMaxLength(128).IsRequired();
			modelBuilder.Entity<Models.Content>().Property(c => c.MetaKeywords).HasMaxLength(128);
			modelBuilder.Entity<Models.Content>().Property(c => c.MetaDescription).HasMaxLength(256);
			modelBuilder.Entity<Models.Content>().Property(c => c.Type)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Slug") {
					Order = 1,
					IsUnique = true
				}));
			modelBuilder.Entity<Models.Content>().Property(c => c.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Slug") {
					Order = 2,
				}));
			modelBuilder.Entity<Models.Content>().HasRequired(c => c.Category).WithMany().WillCascadeOnDelete(false);

			// ContentBlock
			modelBuilder.Entity<Models.ContentBlock>().ToTable("PiranhaContentBlocks");
			modelBuilder.Entity<Models.ContentBlock>().Property(b => b.ClrType).HasMaxLength(512).IsRequired();
			modelBuilder.Entity<Models.ContentBlock>().Ignore(b => b.Body);

			// ContentRow
			modelBuilder.Entity<Models.ContentRow>().ToTable("PiranhaContentRows");

			// Media
			modelBuilder.Entity<Models.Media>().ToTable("PiranhaMedia");
			modelBuilder.Entity<Models.Media>().Property(m => m.Name).HasMaxLength(128).IsRequired();
			modelBuilder.Entity<Models.Media>().Property(m => m.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));
				
			// Param
			modelBuilder.Entity<Models.Param>().ToTable("PiranhaParams");
			modelBuilder.Entity<Models.Param>().Property(p => p.Name).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));

			// Ratings
			modelBuilder.Entity<Models.Rating>().ToTable("PiranhaRatings");
			modelBuilder.Entity<Models.Rating>().Property(r => r.UserId).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserRating") {
					IsUnique = true,
					Order = 1
				}));
			modelBuilder.Entity<Models.Rating>().Property(r => r.ModelId)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserRating") { 
					Order = 2
				}));
			modelBuilder.Entity<Models.Rating>().Property(r => r.Type)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserRating") {
					Order = 3
				}));

			// Template
			modelBuilder.Entity<Models.Template>().ToTable("PiranhaTemplates");
			modelBuilder.Entity<Models.Template>().Property(t => t.Name).HasMaxLength(128).IsRequired();
			modelBuilder.Entity<Models.Template>().Property(t => t.Route).HasMaxLength(255);
			modelBuilder.Entity<Models.Template>().Property(t => t.View).HasMaxLength(255);

			base.OnModelCreating(modelBuilder);
		}
	}
}