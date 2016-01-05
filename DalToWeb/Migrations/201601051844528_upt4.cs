namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upt4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Article_Id", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "Article_Id" });
            RenameColumn(table: "dbo.Comments", name: "Article_Id", newName: "ArticleId");
            AlterColumn("dbo.Comments", "ArticleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ArticleId");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            AlterColumn("dbo.Comments", "ArticleId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "ArticleId", newName: "Article_Id");
            CreateIndex("dbo.Comments", "Article_Id");
            AddForeignKey("dbo.Comments", "Article_Id", "dbo.Articles", "Id");
        }
    }
}
