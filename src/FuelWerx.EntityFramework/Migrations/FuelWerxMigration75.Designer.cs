using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class FuelWerxMigration75 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration75));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602250313482_FuelWerxMigration75";
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

		public FuelWerxMigration75()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxEstimates", "ProjectAcceptedTime", null);
			base.DropColumn("dbo.FuelWerxEstimates", "ProjectId", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxEstimates", "ProjectId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxEstimates", "ProjectAcceptedTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
		}
	}
}