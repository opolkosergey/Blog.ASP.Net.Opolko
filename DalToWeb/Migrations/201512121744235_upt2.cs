namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upt2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Articles", new[] { "BlogId" });
            AlterColumn("dbo.Articles", "BlogId", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "BlogId");
            AddForeignKey("dbo.Articles", "BlogId", "dbo.Blogs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Articles", new[] { "BlogId" });
            AlterColumn("dbo.Articles", "BlogId", c => c.Int());
            CreateIndex("dbo.Articles", "BlogId");
            AddForeignKey("dbo.Articles", "BlogId", "dbo.Blogs", "Id");
        }
    }
}
