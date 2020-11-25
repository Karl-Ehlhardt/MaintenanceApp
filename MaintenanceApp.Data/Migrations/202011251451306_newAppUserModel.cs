namespace MaintenanceApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newAppUserModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TasksForMachine", "UserInfoId", "dbo.UserInfo");
            DropIndex("dbo.TasksForMachine", new[] { "UserInfoId" });
            AddColumn("dbo.TasksForMachine", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TasksForMachine", "ApplicationUserId");
            AddForeignKey("dbo.TasksForMachine", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            DropColumn("dbo.TasksForMachine", "UserInfoId");
            DropTable("dbo.UserInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        UserInfoId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Active = c.Boolean(nullable: false),
                        InactiveDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ReactiveDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.UserInfoId);
            
            AddColumn("dbo.TasksForMachine", "UserInfoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TasksForMachine", "ApplicationUserId", "dbo.ApplicationUser");
            DropIndex("dbo.TasksForMachine", new[] { "ApplicationUserId" });
            DropColumn("dbo.TasksForMachine", "ApplicationUserId");
            CreateIndex("dbo.TasksForMachine", "UserInfoId");
            AddForeignKey("dbo.TasksForMachine", "UserInfoId", "dbo.UserInfo", "UserInfoId");
        }
    }
}
