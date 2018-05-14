using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Renamed_StoredFile_To_BinaryObject : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Renamed_StoredFile_To_BinaryObject));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201504111928297_Renamed_StoredFile_To_BinaryObject";
			}
		}

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Source
		{
			get
			{
				return null;
			}
		}

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Target
		{
			get
			{
				return this.Resources.GetString("Target");
			}
		}

		public Renamed_StoredFile_To_BinaryObject()
		{
		}

		public override void Down()
		{
			base.CreateTable("dbo.StoredFiles", (ColumnBuilder c) => {
				ColumnModel columnModel = c.Guid(new bool?(false), false, null, null, null, null, null);
				int? nullable = null;
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel1 = c.String(new bool?(false), nullable, nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(nullable3, nullable, nullable4, nullable1, null, null, null, null, null);
				nullable = null;
				nullable1 = null;
				return new { Id = columnModel, FileName = columnModel1, FileType = columnModel2, Bytes = c.Binary(new bool?(false), nullable, nullable1, null, null, false, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null);
			base.DropTable("dbo.AbpBinaryObjects");
		}

		public override void Up()
		{
			base.CreateTable("dbo.AbpBinaryObjects", (ColumnBuilder c) => new { Id = c.Guid(new bool?(false), false, null, null, null, null, null), Bytes = c.Binary(new bool?(false), null, null, null, null, false, null, null, null) }, null).PrimaryKey((t) => t.Id, null, true, null);
			base.DropTable("dbo.StoredFiles");
		}
	}
}