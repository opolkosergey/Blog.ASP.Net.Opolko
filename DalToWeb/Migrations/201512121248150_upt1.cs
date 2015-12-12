namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upt1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Blogs", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Blogs", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Blogs", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Blogs", name: "UserId", newName: "User_Id");
        }
    }
}
