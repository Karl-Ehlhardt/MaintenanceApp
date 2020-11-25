namespace MaintenanceApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TasksForMachine", new[] { "ApplicationUserId" });
            AlterColumn("dbo.TasksForMachine", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TasksForMachine", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TasksForMachine", new[] { "ApplicationUserId" });
            AlterColumn("dbo.TasksForMachine", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TasksForMachine", "ApplicationUserId");
        }
    }
}
