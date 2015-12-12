namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArticle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        Content = c.String(),
                        ImagePath = c.String(),
                        BlogId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId)
                .Index(t => t.BlogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Articles", new[] { "BlogId" });
            DropTable("dbo.Articles");
        }
    }
}
