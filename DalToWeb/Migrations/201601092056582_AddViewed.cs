namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Viewed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Viewed");
        }
    }
}
