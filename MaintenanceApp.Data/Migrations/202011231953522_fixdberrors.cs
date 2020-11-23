namespace MaintenanceApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdberrors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admin", "AdminEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admin", "AdminEmail");
        }
    }
}
