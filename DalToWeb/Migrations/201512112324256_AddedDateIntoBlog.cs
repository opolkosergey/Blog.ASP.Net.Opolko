namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateIntoBlog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "TimeAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "TimeAdded");
        }
    }
}
