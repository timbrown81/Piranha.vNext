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

	/// <summary>
	/// Adds the ratings table.
	/// </summary>
	public partial class AddRatings : DbMigration
	{
		public override void Up() {
			CreateTable(
				"dbo.PiranhaRatings",
				c => new {
					Id = c.Guid(nullable: false),
					ModelId = c.Guid(nullable: false),
					UserId = c.String(nullable: false, maxLength: 128),
					Type = c.Int(nullable: false),
					Created = c.DateTime(nullable: false),
					Updated = c.DateTime(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.Index(t => new { t.UserId, t.ModelId, t.Type }, unique: true, name: "IX_UserRating");
		}

		public override void Down() {
			DropIndex("dbo.PiranhaRatings", "IX_UserRating");
			DropTable("dbo.PiranhaRatings");
		}
	}
}
