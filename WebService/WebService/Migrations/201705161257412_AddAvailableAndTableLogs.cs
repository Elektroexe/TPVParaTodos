namespace WebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvailableAndTableLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogsProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product_Id = c.Int(nullable: false),
                        Action = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Products", "Available", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogsProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.LogsProducts", new[] { "Product_Id" });
            DropColumn("dbo.Products", "Available");
            DropTable("dbo.LogsProducts");
        }
    }
}
