namespace Wechflix.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class SeedDefaultUsers : DbMigration
	{
		public override void Up()
		{
			Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4ef38cc3-4f0f-4eef-95fa-48fd7d48de7b', N'Admin@WechFlix.com', 0, N'AChNojA4737+VbwDafvfLMskfVRSozQwgMlFgCfxFaqFDy6xQIRPmVBq23Qcjjyjkw==', N'a22d226f-fe94-44b6-9947-393077b09e5d', NULL, 0, 0, NULL, 1, 0, N'Admin@WechFlix.com')");
			Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8d8f959b-e364-49e2-b012-558905dd29a0', N'Guest@WechFlix.com', 0, N'ABKPIQZ+8Y/ay+GbQlKR7RHDQW0pYxt25+diFGcYkBviDMcVA1PtwlTkGt7hqIuEkA==', N'efd1d9f2-8bfa-419e-9a1e-90cfcde7b7a8', NULL, 0, 0, NULL, 1, 0, N'Guest@WechFlix.com')");
			Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2e4e0a57-7ac3-48fb-8020-4beb3b81feab', N'CanManageMovies')");
			Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4ef38cc3-4f0f-4eef-95fa-48fd7d48de7b', N'2e4e0a57-7ac3-48fb-8020-4beb3b81feab')");
		}
		
		public override void Down()
		{
		}
	}
}
