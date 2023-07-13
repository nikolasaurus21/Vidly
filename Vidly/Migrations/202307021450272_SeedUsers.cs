namespace Vidly.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1c1c39fa-fd5e-4455-b300-411d3214da7c', N'admin@vidly.com', 0, N'AILp90E1+wd7LX1NUic9aAj8tn2q9i8hRrk/B6kyh8KEimWF1IGPf+0l5GhM5ZDHbQ==', N'a0070fa4-f1e8-485f-abab-6c79e58fabc6', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'95f89fde-add3-46ca-a9c6-a167f1261983', N'guest@vidly.com', 0, N'AImtL4Ze33zpqY3fzKG9hvhBY2iPuZ3VTmjm/q8JOw8+OaR9VhaBRzTnu3HMum0lmg==', N'3efd96e4-5f8f-4b50-bbd6-c86c828ecfc9', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f231f492-83de-4d71-853a-f86808278d80', N'CanManageMovies')

                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1c1c39fa-fd5e-4455-b300-411d3214da7c', N'f231f492-83de-4d71-853a-f86808278d80')

            ");
        }

        public override void Down()
        {
        }
    }
}
