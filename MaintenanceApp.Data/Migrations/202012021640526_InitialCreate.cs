namespace MaintenanceApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        AreaName = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.Building", t => t.BuildingId)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Building",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        BuildingName = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BuildingId);
            
            CreateTable(
                "dbo.Machine",
                c => new
                    {
                        MachineId = c.Int(nullable: false, identity: true),
                        MachineName = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MachineId)
                .ForeignKey("dbo.Area", t => t.AreaId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.MaintenanceTask",
                c => new
                    {
                        MaintenanceTaskId = c.Int(nullable: false, identity: true),
                        MaintenanceTaskName = c.String(nullable: false),
                        MaintenanceTaskDescription = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        MaintenanceTaskInterval = c.Long(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        MachineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaintenanceTaskId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .ForeignKey("dbo.Machine", t => t.MachineId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.MachineId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Admin = c.Boolean(nullable: false),
                        AreaId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        InactiveDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ReactivatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TasksForMachine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MachineId = c.Int(nullable: false),
                        Maintained = c.DateTimeOffset(nullable: false, precision: 7),
                        NeedToBeMaintainedBy = c.DateTimeOffset(nullable: false, precision: 7),
                        MaintenanceTaskId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .ForeignKey("dbo.Machine", t => t.MachineId)
                .ForeignKey("dbo.MaintenanceTask", t => t.MaintenanceTaskId)
                .Index(t => t.MachineId)
                .Index(t => t.MaintenanceTaskId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TasksForMachine", "MaintenanceTaskId", "dbo.MaintenanceTask");
            DropForeignKey("dbo.TasksForMachine", "MachineId", "dbo.Machine");
            DropForeignKey("dbo.TasksForMachine", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.MaintenanceTask", "MachineId", "dbo.Machine");
            DropForeignKey("dbo.MaintenanceTask", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Machine", "AreaId", "dbo.Area");
            DropForeignKey("dbo.Area", "BuildingId", "dbo.Building");
            DropIndex("dbo.TasksForMachine", new[] { "ApplicationUserId" });
            DropIndex("dbo.TasksForMachine", new[] { "MaintenanceTaskId" });
            DropIndex("dbo.TasksForMachine", new[] { "MachineId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MaintenanceTask", new[] { "MachineId" });
            DropIndex("dbo.MaintenanceTask", new[] { "ApplicationUserId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Machine", new[] { "AreaId" });
            DropIndex("dbo.Area", new[] { "BuildingId" });
            DropTable("dbo.TasksForMachine");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.MaintenanceTask");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Machine");
            DropTable("dbo.Building");
            DropTable("dbo.Area");
        }
    }
}
