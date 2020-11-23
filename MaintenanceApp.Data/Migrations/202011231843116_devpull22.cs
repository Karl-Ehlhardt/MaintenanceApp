namespace MaintenanceApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class devpull22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TasksForMachine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MachineId = c.Int(nullable: false),
                        Maintained = c.DateTimeOffset(nullable: false, precision: 7),
                        NeedToBeMaintainedBy = c.DateTimeOffset(nullable: false, precision: 7),
                        MaintenanceTaskId = c.Int(nullable: false),
                        UserInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Machine", t => t.MachineId, cascadeDelete: true)
                .ForeignKey("dbo.MaintenanceTask", t => t.MaintenanceTaskId, cascadeDelete: true)
                .ForeignKey("dbo.UserInfo", t => t.UserInfoId, cascadeDelete: true)
                .Index(t => t.MachineId)
                .Index(t => t.MaintenanceTaskId)
                .Index(t => t.UserInfoId);
            
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
            
            AddColumn("dbo.ApplicationUser", "StartDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ApplicationUser", "Admin", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "AreaId", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationUser", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "InactiveDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ApplicationUser", "ReactivatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TasksForMachine", "UserInfoId", "dbo.UserInfo");
            DropForeignKey("dbo.TasksForMachine", "MaintenanceTaskId", "dbo.MaintenanceTask");
            DropForeignKey("dbo.TasksForMachine", "MachineId", "dbo.Machine");
            DropIndex("dbo.TasksForMachine", new[] { "UserInfoId" });
            DropIndex("dbo.TasksForMachine", new[] { "MaintenanceTaskId" });
            DropIndex("dbo.TasksForMachine", new[] { "MachineId" });
            DropColumn("dbo.ApplicationUser", "ReactivatedDate");
            DropColumn("dbo.ApplicationUser", "InactiveDate");
            DropColumn("dbo.ApplicationUser", "Active");
            DropColumn("dbo.ApplicationUser", "AreaId");
            DropColumn("dbo.ApplicationUser", "Admin");
            DropColumn("dbo.ApplicationUser", "StartDate");
            DropTable("dbo.UserInfo");
            DropTable("dbo.TasksForMachine");
        }
    }
}
