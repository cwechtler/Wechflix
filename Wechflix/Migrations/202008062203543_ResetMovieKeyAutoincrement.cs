namespace Wechflix.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class ResetMovieKeyAutoincrement : DbMigration
	{
		public override void Up()
		{
			Sql("dbcc checkident (Movies, reseed, 10)");
		}
		
		public override void Down()
		{
		}
	}
}
