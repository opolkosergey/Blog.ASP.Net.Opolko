namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentsToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Article_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Article_Id");
            AddForeignKey("dbo.Comments", "Article_Id", "dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Article_Id", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "Article_Id" });
            DropColumn("dbo.Comments", "Article_Id");
        }
    }
}
