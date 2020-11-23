namespace MaintenanceApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaintenanceTask", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.MaintenanceTask", "ApplicationUserId");
            AddForeignKey("dbo.MaintenanceTask", "ApplicationUserId", "dbo.ApplicationUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaintenanceTask", "ApplicationUserId", "dbo.ApplicationUser");
            DropIndex("dbo.MaintenanceTask", new[] { "ApplicationUserId" });
            DropColumn("dbo.MaintenanceTask", "ApplicationUserId");
        }
    }
}
