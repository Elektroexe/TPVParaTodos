namespace WebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fragments",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Order_Id = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Order_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Commentary = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Table_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tables", t => t.Table_Id, cascadeDelete: true)
                .Index(t => t.Table_Id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaxPeople = c.Int(nullable: false),
                        Empty = c.Boolean(nullable: false),
                        Zone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zones", t => t.Zone_Id)
                .Index(t => t.Zone_Id);
            
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Waiter_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Waiters", t => t.Waiter_Id)
                .Index(t => t.Waiter_Id);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        LastConnection = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MenuDrinks",
                c => new
                    {
                        Menu_Id = c.Int(nullable: false),
                        Drink_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Menu_Id, t.Drink_Id })
                .ForeignKey("dbo.Menus", t => t.Menu_Id)
                .ForeignKey("dbo.Drinks", t => t.Drink_Id)
                .Index(t => t.Menu_Id)
                .Index(t => t.Drink_Id);
            
            CreateTable(
                "dbo.MenuFoods",
                c => new
                    {
                        Menu_Id = c.Int(nullable: false),
                        Food_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Menu_Id, t.Food_Id })
                .ForeignKey("dbo.Menus", t => t.Menu_Id)
                .ForeignKey("dbo.Foods", t => t.Food_Id)
                .Index(t => t.Menu_Id)
                .Index(t => t.Food_Id);
            
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        TypeBottle = c.String(),
                        Soda = c.Boolean(nullable: false),
                        Alcohol = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FamilyDish = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PeopleNumber = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Waiters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staff", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Waiters", "Id", "dbo.Staff");
            DropForeignKey("dbo.Menus", "Id", "dbo.Products");
            DropForeignKey("dbo.Foods", "Id", "dbo.Products");
            DropForeignKey("dbo.Drinks", "Id", "dbo.Products");
            DropForeignKey("dbo.MenuFoods", "Food_Id", "dbo.Foods");
            DropForeignKey("dbo.MenuFoods", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.MenuDrinks", "Drink_Id", "dbo.Drinks");
            DropForeignKey("dbo.MenuDrinks", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.Fragments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Zones", "Waiter_Id", "dbo.Waiters");
            DropForeignKey("dbo.Staff", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tables", "Zone_Id", "dbo.Zones");
            DropForeignKey("dbo.Orders", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.Fragments", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Waiters", new[] { "Id" });
            DropIndex("dbo.Menus", new[] { "Id" });
            DropIndex("dbo.Foods", new[] { "Id" });
            DropIndex("dbo.Drinks", new[] { "Id" });
            DropIndex("dbo.MenuFoods", new[] { "Food_Id" });
            DropIndex("dbo.MenuFoods", new[] { "Menu_Id" });
            DropIndex("dbo.MenuDrinks", new[] { "Drink_Id" });
            DropIndex("dbo.MenuDrinks", new[] { "Menu_Id" });
            DropIndex("dbo.Staff", new[] { "Id" });
            DropIndex("dbo.Zones", new[] { "Waiter_Id" });
            DropIndex("dbo.Tables", new[] { "Zone_Id" });
            DropIndex("dbo.Orders", new[] { "Table_Id" });
            DropIndex("dbo.Fragments", new[] { "Order_Id" });
            DropIndex("dbo.Fragments", new[] { "Product_Id" });
            DropTable("dbo.Waiters");
            DropTable("dbo.Menus");
            DropTable("dbo.Foods");
            DropTable("dbo.Drinks");
            DropTable("dbo.MenuFoods");
            DropTable("dbo.MenuDrinks");
            DropTable("dbo.Staff");
            DropTable("dbo.Zones");
            DropTable("dbo.Tables");
            DropTable("dbo.Orders");
            DropTable("dbo.Fragments");
            DropTable("dbo.Products");
        }
    }
}
