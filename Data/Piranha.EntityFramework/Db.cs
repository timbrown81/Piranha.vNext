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
		/// Gets/sets the content field set.
		/// </summary>
		public DbSet<Models.ContentField> ContentFields { get; set; }

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
		/// Gets/sets the tag set.
		/// </summary>
		public DbSet<Models.Tag> Tags { get; set; }

		/// <summary>
		/// Gets/sets the template set.
		/// </summary>
		public DbSet<Models.Template> Templates { get; set; }

		/// <summary>
		/// Gets/sets the template fields.
		/// </summary>
		public DbSet<Models.TemplateField> TemplateFields { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Db() : base("piranha", new MigrateDatabaseToLatestVersion<Db, Migrations.Configuration>()) {}

		/// <summary>
		/// Configures the data model.
		/// </summary>
		/// <param name="mb">The current model builder</param>
		protected override void OnModelCreating(DbModelBuilder mb) {
			// Alias
			mb.Entity<Models.Alias>().ToTable("PiranhaAliases");
			mb.Entity<Models.Alias>().Property(a => a.NewUrl).HasMaxLength(255).IsRequired();
			mb.Entity<Models.Alias>().Property(a => a.OldUrl).HasMaxLength(255).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));

			// Author
			mb.Entity<Models.Author>().ToTable("PiranhaAuthors");
			mb.Entity<Models.Author>().Property(a => a.Name).HasMaxLength(128).IsRequired();
			mb.Entity<Models.Author>().Property(a => a.Email).HasMaxLength(128);
			mb.Entity<Models.Author>().Property(a => a.Description).HasMaxLength(512);

			// Block
			mb.Entity<Models.Block>().ToTable("PiranhaBlocks");
			mb.Entity<Models.Block>().Property(b => b.Name).HasMaxLength(128).IsRequired();
			mb.Entity<Models.Block>().Property(b => b.Description).HasMaxLength(255);
			mb.Entity<Models.Block>().Property(b => b.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));
	
			// Category
			mb.Entity<Models.Category>().ToTable("PiranhaCategories");
			mb.Entity<Models.Category>().Property(c => c.Title).HasMaxLength(128).IsRequired();
			mb.Entity<Models.Category>().Property(c => c.ArchiveTitle).HasMaxLength(128);
			mb.Entity<Models.Category>().Property(c => c.MetaKeywords).HasMaxLength(128);
			mb.Entity<Models.Category>().Property(c => c.MetaDescription).HasMaxLength(255);
			mb.Entity<Models.Category>().Property(c => c.ArchiveView).HasMaxLength(255);
			mb.Entity<Models.Category>().Property(c => c.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));
			mb.Entity<Models.Category>().Ignore(c => c.ArchiveSlug);

			// Comment
			mb.Entity<Models.Comment>().ToTable("PiranhaComments");
			mb.Entity<Models.Comment>().Property(c => c.UserId).HasMaxLength(128);
			mb.Entity<Models.Comment>().Property(c => c.Author).HasMaxLength(128);
			mb.Entity<Models.Comment>().Property(c => c.Email).HasMaxLength(128);
			mb.Entity<Models.Comment>().Property(c => c.IP).HasMaxLength(16);
			mb.Entity<Models.Comment>().Property(c => c.UserAgent).HasMaxLength(128);
			mb.Entity<Models.Comment>().Property(c => c.WebSite).HasMaxLength(128);
			mb.Entity<Models.Comment>().Property(c => c.SessionID).HasMaxLength(64);

			// Content
			mb.Entity<Models.Content>().ToTable("PiranhaContent");
			mb.Entity<Models.Content>().Property(c => c.Title).HasMaxLength(128).IsRequired();
			mb.Entity<Models.Content>().Property(c => c.MetaKeywords).HasMaxLength(128);
			mb.Entity<Models.Content>().Property(c => c.MetaDescription).HasMaxLength(256);
			mb.Entity<Models.Content>().Property(c => c.Type)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Slug") {
					Order = 1,
					IsUnique = true
				}));
			mb.Entity<Models.Content>().Property(c => c.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Slug") {
					Order = 2,
				}));
			mb.Entity<Models.Content>().HasRequired(c => c.Category).WithMany().WillCascadeOnDelete(false);
			mb.Entity<Models.Content>().HasMany(c => c.Tags).WithMany().Map(m => {
				m.ToTable("PiranhaContentTags");
				m.MapLeftKey("ContentId");
				m.MapRightKey("TagId");
			});

			// ContentBlock
			mb.Entity<Models.ContentBlock>().ToTable("PiranhaContentBlocks");
			mb.Entity<Models.ContentBlock>().Property(b => b.CLRType).HasMaxLength(512).IsRequired();
			mb.Entity<Models.ContentBlock>().Ignore(b => b.Body);

			// ContentField
			mb.Entity<Models.ContentField>().ToTable("PiranhaContentFields");
			mb.Entity<Models.ContentField>().Property(f => f.CLRType).HasMaxLength(512).IsRequired();

			// ContentRow
			mb.Entity<Models.ContentRow>().ToTable("PiranhaContentRows");

			// Media
			mb.Entity<Models.Media>().ToTable("PiranhaMedia");
			mb.Entity<Models.Media>().Property(m => m.Name).HasMaxLength(128).IsRequired();
			mb.Entity<Models.Media>().Property(m => m.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));
				
			// Param
			mb.Entity<Models.Param>().ToTable("PiranhaParams");
			mb.Entity<Models.Param>().Property(p => p.Name).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));

			// Rating
			mb.Entity<Models.Rating>().ToTable("PiranhaRatings");
			mb.Entity<Models.Rating>().Property(r => r.UserId).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserRating") {
					IsUnique = true,
					Order = 1
				}));
			mb.Entity<Models.Rating>().Property(r => r.ModelId)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserRating") { 
					Order = 2
				}));
			mb.Entity<Models.Rating>().Property(r => r.Type)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserRating") {
					Order = 3
				}));

			// Tag
			mb.Entity<Models.Tag>().ToTable("PiranhaTags");
			mb.Entity<Models.Tag>().Property(t => t.Title).HasMaxLength(128).IsRequired();
			mb.Entity<Models.Tag>().Property(t => t.ArchiveTitle).HasMaxLength(128);
			mb.Entity<Models.Tag>().Property(t => t.MetaKeywords).HasMaxLength(128);
			mb.Entity<Models.Tag>().Property(t => t.MetaDescription).HasMaxLength(255);
			mb.Entity<Models.Tag>().Property(t => t.ArchiveView).HasMaxLength(255);
			mb.Entity<Models.Tag>().Property(t => t.Slug).HasMaxLength(128).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {
					IsUnique = true
				}));
			mb.Entity<Models.Tag>().Ignore(t => t.ArchiveSlug);

			// Template
			mb.Entity<Models.Template>().ToTable("PiranhaTemplates");
			mb.Entity<Models.Template>().Property(t => t.Name).HasMaxLength(128).IsRequired();
			mb.Entity<Models.Template>().Property(t => t.Route).HasMaxLength(255);
			mb.Entity<Models.Template>().Property(t => t.View).HasMaxLength(255);

			// TemplateField
			mb.Entity<Models.TemplateField>().ToTable("PiranhaTemplateFields");
			mb.Entity<Models.TemplateField>().Property(f => f.TemplateId)
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_InternalId") {
					IsUnique = true,
					Order = 1
				}));
			mb.Entity<Models.TemplateField>().Property(f => f.InternalId).HasMaxLength(32).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_InternalId") {
					Order = 2
				}));
			mb.Entity<Models.TemplateField>().Property(f => f.Name).HasMaxLength(128).IsRequired();
			mb.Entity<Models.TemplateField>().Property(f => f.CLRType).HasMaxLength(512).IsRequired();

			base.OnModelCreating(mb);
		}
	}
}