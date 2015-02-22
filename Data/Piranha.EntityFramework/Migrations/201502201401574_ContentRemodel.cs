/*
 * Copyright (c) 2014 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

namespace Piranha.EntityFramework.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class ContentRemodel : DbMigration
	{
		public override void Up() {
			DropForeignKey("dbo.PiranhaPostMedia", "Post_Id", "dbo.PiranhaPosts");
			DropForeignKey("dbo.PiranhaPostMedia", "Media_Id", "dbo.PiranhaMedia");
			DropForeignKey("dbo.PiranhaPosts", "AuthorId", "dbo.PiranhaAuthors");
			DropForeignKey("dbo.PiranhaPostCategories", "Post_Id", "dbo.PiranhaPosts");
			DropForeignKey("dbo.PiranhaPostCategories", "Category_Id", "dbo.PiranhaCategories");
			DropForeignKey("dbo.PiranhaComments", "PostId", "dbo.PiranhaPosts");
			DropForeignKey("dbo.PiranhaPosts", "TypeId", "dbo.PiranhaPostTypes");
			DropForeignKey("dbo.PiranhaPages", "AuthorId", "dbo.PiranhaAuthors");
			DropForeignKey("dbo.PiranhaPageTypeProperties", "TypeId", "dbo.PiranhaPageTypes");
			DropForeignKey("dbo.PiranhaPageTypeRegions", "TypeId", "dbo.PiranhaPageTypes");
			DropForeignKey("dbo.PiranhaPages", "TypeId", "dbo.PiranhaPageTypes");
			DropIndex("dbo.PiranhaComments", new[] { "PostId" });
			DropIndex("dbo.PiranhaPosts", "IX_Slug");
			DropIndex("dbo.PiranhaPosts", new[] { "AuthorId" });
			DropIndex("dbo.PiranhaPostTypes", new[] { "Slug" });
			DropIndex("dbo.PiranhaPages", new[] { "TypeId" });
			DropIndex("dbo.PiranhaPages", new[] { "Slug" });
			DropIndex("dbo.PiranhaPages", new[] { "AuthorId" });
			DropIndex("dbo.PiranhaPageTypes", new[] { "Slug" });
			DropIndex("dbo.PiranhaPageTypeProperties", "IX_InternalId");
			DropIndex("dbo.PiranhaPageTypeRegions", "IX_InternalId");
			DropIndex("dbo.PiranhaPostMedia", new[] { "Post_Id" });
			DropIndex("dbo.PiranhaPostMedia", new[] { "Media_Id" });
			DropIndex("dbo.PiranhaPostCategories", new[] { "Post_Id" });
			DropIndex("dbo.PiranhaPostCategories", new[] { "Category_Id" });
			CreateTable(
				"dbo.PiranhaContent",
				c => new {
					Id = c.Guid(nullable: false),
					CategoryId = c.Guid(nullable: false),
					TemplateId = c.Guid(),
					AuthorId = c.Guid(),
					Type = c.Int(nullable: false),
					Title = c.String(nullable: false, maxLength: 128),
					Slug = c.String(nullable: false, maxLength: 128),
					MetaKeywords = c.String(maxLength: 128),
					MetaDescription = c.String(maxLength: 256),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
					Published = c.DateTime(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.PiranhaAuthors", t => t.AuthorId)
				.ForeignKey("dbo.PiranhaCategories", t => t.CategoryId)
				.ForeignKey("dbo.PiranhaTemplates", t => t.TemplateId)
				.Index(t => t.CategoryId)
				.Index(t => t.TemplateId)
				.Index(t => t.AuthorId)
				.Index(t => new { t.Type, t.Slug }, unique: true, name: "IX_Slug");

			CreateTable(
				"dbo.PiranhaContentRows",
				c => new {
					Id = c.Guid(nullable: false),
					ContentId = c.Guid(nullable: false),
					SortOrder = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.PiranhaContent", t => t.ContentId, cascadeDelete: true)
				.Index(t => t.ContentId);

			CreateTable(
				"dbo.PiranhaContentBlocks",
				c => new {
					Id = c.Guid(nullable: false),
					RowId = c.Guid(nullable: false),
					SortOrder = c.Int(nullable: false),
					Size = c.Int(nullable: false),
					CLRType = c.String(nullable: false, maxLength: 512),
					Value = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.PiranhaContentRows", t => t.RowId, cascadeDelete: true)
				.Index(t => t.RowId);

			CreateTable(
				"dbo.PiranhaContentFields",
				c => new {
					Id = c.Guid(nullable: false),
					ContentId = c.Guid(nullable: false),
					TemplateFieldId = c.Guid(nullable: false),
					CLRType = c.String(nullable: false, maxLength: 512),
					Value = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.PiranhaContent", t => t.ContentId, cascadeDelete: true)
				.ForeignKey("dbo.PiranhaTemplateFields", t => t.TemplateFieldId, cascadeDelete: true)
				.Index(t => t.ContentId)
				.Index(t => t.TemplateFieldId);

			CreateTable(
				"dbo.PiranhaTemplateFields",
				c => new {
					Id = c.Guid(nullable: false),
					TemplateId = c.Guid(nullable: false),
					InternalId = c.String(nullable: false, maxLength: 32),
					Name = c.String(nullable: false, maxLength: 128),
					CLRType = c.String(nullable: false, maxLength: 512),
					IsCollection = c.Boolean(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.PiranhaTemplates", t => t.TemplateId, cascadeDelete: true)
				.Index(t => new { t.TemplateId, t.InternalId }, unique: true, name: "IX_InternalId");

			CreateTable(
				"dbo.PiranhaTemplates",
				c => new {
					Id = c.Guid(nullable: false),
					Type = c.Int(nullable: false),
					Name = c.String(nullable: false, maxLength: 128),
					Route = c.String(maxLength: 255),
					View = c.String(maxLength: 255),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.PiranhaTags",
				c => new {
					Id = c.Guid(nullable: false),
					Title = c.String(nullable: false, maxLength: 128),
					Slug = c.String(nullable: false, maxLength: 128),
					ArchiveTitle = c.String(maxLength: 128),
					MetaKeywords = c.String(maxLength: 128),
					MetaDescription = c.String(maxLength: 255),
					ArchiveView = c.String(maxLength: 255),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Slug, unique: true);

			CreateTable(
				"dbo.PiranhaContentTags",
				c => new {
					ContentId = c.Guid(nullable: false),
					TagId = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.ContentId, t.TagId })
				.ForeignKey("dbo.PiranhaContent", t => t.ContentId, cascadeDelete: true)
				.ForeignKey("dbo.PiranhaTags", t => t.TagId, cascadeDelete: true)
				.Index(t => t.ContentId)
				.Index(t => t.TagId);

			AddColumn("dbo.PiranhaCategories", "ArchiveTitle", c => c.String(maxLength: 128));
			AddColumn("dbo.PiranhaCategories", "MetaKeywords", c => c.String(maxLength: 128));
			AddColumn("dbo.PiranhaCategories", "MetaDescription", c => c.String(maxLength: 255));
			AddColumn("dbo.PiranhaCategories", "ArchiveView", c => c.String(maxLength: 255));
			AddColumn("dbo.PiranhaComments", "ContentId", c => c.Guid(nullable: false));
			CreateIndex("dbo.PiranhaComments", "ContentId");
			AddForeignKey("dbo.PiranhaComments", "ContentId", "dbo.PiranhaContent", "Id", cascadeDelete: true);
			DropColumn("dbo.PiranhaComments", "PostId");
			DropTable("dbo.PiranhaPosts");
			DropTable("dbo.PiranhaPostTypes");
			DropTable("dbo.PiranhaPages");
			DropTable("dbo.PiranhaPageTypes");
			DropTable("dbo.PiranhaPageTypeProperties");
			DropTable("dbo.PiranhaPageTypeRegions");
			DropTable("dbo.PiranhaPostMedia");
			DropTable("dbo.PiranhaPostCategories");
		}

		public override void Down() {
			CreateTable(
				"dbo.PiranhaPostCategories",
				c => new {
					Post_Id = c.Guid(nullable: false),
					Category_Id = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.Post_Id, t.Category_Id });

			CreateTable(
				"dbo.PiranhaPostMedia",
				c => new {
					Post_Id = c.Guid(nullable: false),
					Media_Id = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.Post_Id, t.Media_Id });

			CreateTable(
				"dbo.PiranhaPageTypeRegions",
				c => new {
					Id = c.Guid(nullable: false),
					TypeId = c.Guid(nullable: false),
					InternalId = c.String(nullable: false, maxLength: 32),
					Name = c.String(nullable: false, maxLength: 128),
					CLRType = c.String(nullable: false, maxLength: 512),
					IsCollection = c.Boolean(nullable: false),
					Order = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.PiranhaPageTypeProperties",
				c => new {
					Id = c.Guid(nullable: false),
					TypeId = c.Guid(nullable: false),
					InternalId = c.String(nullable: false, maxLength: 32),
					Name = c.String(nullable: false, maxLength: 128),
					CLRType = c.String(nullable: false, maxLength: 512),
					IsCollection = c.Boolean(nullable: false),
					Order = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.PiranhaPageTypes",
				c => new {
					Id = c.Guid(nullable: false),
					Slug = c.String(nullable: false, maxLength: 128),
					Name = c.String(nullable: false, maxLength: 128),
					Description = c.String(maxLength: 255),
					Route = c.String(maxLength: 255),
					View = c.String(maxLength: 255),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.PiranhaPages",
				c => new {
					Id = c.Guid(nullable: false),
					TypeId = c.Guid(nullable: false),
					Slug = c.String(nullable: false, maxLength: 128),
					ParentId = c.Guid(),
					SortOrder = c.Int(nullable: false),
					IsHidden = c.Boolean(nullable: false),
					NavigationTitle = c.String(maxLength: 128),
					Body = c.String(),
					AuthorId = c.Guid(nullable: false),
					Title = c.String(nullable: false, maxLength: 128),
					Keywords = c.String(maxLength: 128),
					Description = c.String(maxLength: 255),
					Route = c.String(maxLength: 255),
					View = c.String(maxLength: 255),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
					Published = c.DateTime(),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.PiranhaPostTypes",
				c => new {
					Id = c.Guid(nullable: false),
					IncludeInRss = c.Boolean(nullable: false),
					EnableArchive = c.Boolean(nullable: false),
					MetaKeywords = c.String(maxLength: 128),
					MetaDescription = c.String(maxLength: 255),
					ArchiveTitle = c.String(maxLength: 128),
					ArchiveRoute = c.String(maxLength: 255),
					ArchiveView = c.String(maxLength: 255),
					CommentRoute = c.String(maxLength: 255),
					Slug = c.String(nullable: false, maxLength: 128),
					Name = c.String(nullable: false, maxLength: 128),
					Description = c.String(maxLength: 255),
					Route = c.String(maxLength: 255),
					View = c.String(maxLength: 255),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.PiranhaPosts",
				c => new {
					Id = c.Guid(nullable: false),
					TypeId = c.Guid(nullable: false),
					Slug = c.String(nullable: false, maxLength: 128),
					Excerpt = c.String(maxLength: 512),
					Body = c.String(),
					AuthorId = c.Guid(nullable: false),
					Title = c.String(nullable: false, maxLength: 128),
					Keywords = c.String(maxLength: 128),
					Description = c.String(maxLength: 255),
					Route = c.String(maxLength: 255),
					View = c.String(maxLength: 255),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
					Published = c.DateTime(),
				})
				.PrimaryKey(t => t.Id);

			AddColumn("dbo.PiranhaComments", "PostId", c => c.Guid(nullable: false));
			DropForeignKey("dbo.PiranhaContent", "TemplateId", "dbo.PiranhaTemplates");
			DropForeignKey("dbo.PiranhaContentTags", "TagId", "dbo.PiranhaTags");
			DropForeignKey("dbo.PiranhaContentTags", "ContentId", "dbo.PiranhaContent");
			DropForeignKey("dbo.PiranhaContentFields", "TemplateFieldId", "dbo.PiranhaTemplateFields");
			DropForeignKey("dbo.PiranhaTemplateFields", "TemplateId", "dbo.PiranhaTemplates");
			DropForeignKey("dbo.PiranhaContentFields", "ContentId", "dbo.PiranhaContent");
			DropForeignKey("dbo.PiranhaComments", "ContentId", "dbo.PiranhaContent");
			DropForeignKey("dbo.PiranhaContent", "CategoryId", "dbo.PiranhaCategories");
			DropForeignKey("dbo.PiranhaContentRows", "ContentId", "dbo.PiranhaContent");
			DropForeignKey("dbo.PiranhaContentBlocks", "RowId", "dbo.PiranhaContentRows");
			DropForeignKey("dbo.PiranhaContent", "AuthorId", "dbo.PiranhaAuthors");
			DropIndex("dbo.PiranhaContentTags", new[] { "TagId" });
			DropIndex("dbo.PiranhaContentTags", new[] { "ContentId" });
			DropIndex("dbo.PiranhaTags", new[] { "Slug" });
			DropIndex("dbo.PiranhaTemplateFields", "IX_InternalId");
			DropIndex("dbo.PiranhaContentFields", new[] { "TemplateFieldId" });
			DropIndex("dbo.PiranhaContentFields", new[] { "ContentId" });
			DropIndex("dbo.PiranhaContentBlocks", new[] { "RowId" });
			DropIndex("dbo.PiranhaContentRows", new[] { "ContentId" });
			DropIndex("dbo.PiranhaContent", "IX_Slug");
			DropIndex("dbo.PiranhaContent", new[] { "AuthorId" });
			DropIndex("dbo.PiranhaContent", new[] { "TemplateId" });
			DropIndex("dbo.PiranhaContent", new[] { "CategoryId" });
			DropIndex("dbo.PiranhaComments", new[] { "ContentId" });
			DropColumn("dbo.PiranhaComments", "ContentId");
			DropColumn("dbo.PiranhaCategories", "ArchiveView");
			DropColumn("dbo.PiranhaCategories", "MetaDescription");
			DropColumn("dbo.PiranhaCategories", "MetaKeywords");
			DropColumn("dbo.PiranhaCategories", "ArchiveTitle");
			DropTable("dbo.PiranhaContentTags");
			DropTable("dbo.PiranhaTags");
			DropTable("dbo.PiranhaTemplates");
			DropTable("dbo.PiranhaTemplateFields");
			DropTable("dbo.PiranhaContentFields");
			DropTable("dbo.PiranhaContentBlocks");
			DropTable("dbo.PiranhaContentRows");
			DropTable("dbo.PiranhaContent");
			CreateIndex("dbo.PiranhaPostCategories", "Category_Id");
			CreateIndex("dbo.PiranhaPostCategories", "Post_Id");
			CreateIndex("dbo.PiranhaPostMedia", "Media_Id");
			CreateIndex("dbo.PiranhaPostMedia", "Post_Id");
			CreateIndex("dbo.PiranhaPageTypeRegions", new[] { "TypeId", "InternalId" }, unique: true, name: "IX_InternalId");
			CreateIndex("dbo.PiranhaPageTypeProperties", new[] { "TypeId", "InternalId" }, unique: true, name: "IX_InternalId");
			CreateIndex("dbo.PiranhaPageTypes", "Slug", unique: true);
			CreateIndex("dbo.PiranhaPages", "AuthorId");
			CreateIndex("dbo.PiranhaPages", "Slug", unique: true);
			CreateIndex("dbo.PiranhaPages", "TypeId");
			CreateIndex("dbo.PiranhaPostTypes", "Slug", unique: true);
			CreateIndex("dbo.PiranhaPosts", "AuthorId");
			CreateIndex("dbo.PiranhaPosts", new[] { "TypeId", "Slug" }, unique: true, name: "IX_Slug");
			CreateIndex("dbo.PiranhaComments", "PostId");
			AddForeignKey("dbo.PiranhaPages", "TypeId", "dbo.PiranhaPageTypes", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPageTypeRegions", "TypeId", "dbo.PiranhaPageTypes", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPageTypeProperties", "TypeId", "dbo.PiranhaPageTypes", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPages", "AuthorId", "dbo.PiranhaAuthors", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPosts", "TypeId", "dbo.PiranhaPostTypes", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaComments", "PostId", "dbo.PiranhaPosts", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPostCategories", "Category_Id", "dbo.PiranhaCategories", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPostCategories", "Post_Id", "dbo.PiranhaPosts", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPosts", "AuthorId", "dbo.PiranhaAuthors", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPostMedia", "Media_Id", "dbo.PiranhaMedia", "Id", cascadeDelete: true);
			AddForeignKey("dbo.PiranhaPostMedia", "Post_Id", "dbo.PiranhaPosts", "Id", cascadeDelete: true);
		}
	}
}
