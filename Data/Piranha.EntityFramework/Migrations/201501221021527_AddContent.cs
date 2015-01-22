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

	public partial class AddContent : DbMigration
	{
		public override void Up() {
			CreateTable(
				"dbo.PiranhaContent",
				c => new {
					Id = c.Guid(nullable: false),
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
				.ForeignKey("dbo.PiranhaTemplates", t => t.TemplateId)
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
					ClrType = c.String(nullable: false, maxLength: 512),
					Value = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.PiranhaContentRows", t => t.RowId, cascadeDelete: true)
				.Index(t => t.RowId);

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
				"dbo.PiranhaContentCategories",
				c => new {
					Content_Id = c.Guid(nullable: false),
					Category_Id = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.Content_Id, t.Category_Id })
				.ForeignKey("dbo.PiranhaContent", t => t.Content_Id, cascadeDelete: true)
				.ForeignKey("dbo.PiranhaCategories", t => t.Category_Id, cascadeDelete: true)
				.Index(t => t.Content_Id)
				.Index(t => t.Category_Id);

			AddColumn("dbo.PiranhaComments", "Content_Id", c => c.Guid());
			CreateIndex("dbo.PiranhaComments", "Content_Id");
			AddForeignKey("dbo.PiranhaComments", "Content_Id", "dbo.PiranhaContent", "Id");
		}

		public override void Down() {
			DropForeignKey("dbo.PiranhaContent", "TemplateId", "dbo.PiranhaTemplates");
			DropForeignKey("dbo.PiranhaContentRows", "ContentId", "dbo.PiranhaContent");
			DropForeignKey("dbo.PiranhaContentBlocks", "RowId", "dbo.PiranhaContentRows");
			DropForeignKey("dbo.PiranhaComments", "Content_Id", "dbo.PiranhaContent");
			DropForeignKey("dbo.PiranhaContentCategories", "Category_Id", "dbo.PiranhaCategories");
			DropForeignKey("dbo.PiranhaContentCategories", "Content_Id", "dbo.PiranhaContent");
			DropForeignKey("dbo.PiranhaContent", "AuthorId", "dbo.PiranhaAuthors");
			DropIndex("dbo.PiranhaContentCategories", new[] { "Category_Id" });
			DropIndex("dbo.PiranhaContentCategories", new[] { "Content_Id" });
			DropIndex("dbo.PiranhaContentBlocks", new[] { "RowId" });
			DropIndex("dbo.PiranhaContentRows", new[] { "ContentId" });
			DropIndex("dbo.PiranhaContent", "IX_Slug");
			DropIndex("dbo.PiranhaContent", new[] { "AuthorId" });
			DropIndex("dbo.PiranhaContent", new[] { "TemplateId" });
			DropIndex("dbo.PiranhaComments", new[] { "Content_Id" });
			DropColumn("dbo.PiranhaComments", "Content_Id");
			DropTable("dbo.PiranhaContentCategories");
			DropTable("dbo.PiranhaTemplates");
			DropTable("dbo.PiranhaContentBlocks");
			DropTable("dbo.PiranhaContentRows");
			DropTable("dbo.PiranhaContent");
		}
	}
}
