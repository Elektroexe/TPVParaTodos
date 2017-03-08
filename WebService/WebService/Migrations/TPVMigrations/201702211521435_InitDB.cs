namespace WebService.Migrations.TPVMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drink",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30, fixedLength: true),
                        price = c.Decimal(nullable: false, precision: 6, scale: 2),
                        commentary = c.String(maxLength: 300, fixedLength: true),
                        capacity = c.Int(),
                        typeBottle = c.String(maxLength: 20, fixedLength: true),
                        soda = c.Boolean(),
                        alcohol = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, fixedLength: true),
                        price = c.Decimal(nullable: false, precision: 7, scale: 2),
                        peopleNumber = c.Short(nullable: false),
                        description = c.String(maxLength: 300, fixedLength: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Food",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30, fixedLength: true),
                        price = c.Decimal(nullable: false, precision: 6, scale: 2),
                        partOfMenu = c.String(maxLength: 20, fixedLength: true),
                        familyDish = c.String(maxLength: 20, fixedLength: true),
                        commentary = c.String(maxLength: 300, fixedLength: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        table_id = c.Int(nullable: false),
                        total = c.Decimal(precision: 8, scale: 2),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        employee_id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Table", t => t.table_id)
                .Index(t => t.table_id);
            
            CreateTable(
                "dbo.Table",
                c => new
                    {
                        id = c.Int(nullable: false),
                        maxPeople = c.Int(),
                        location = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Menu_has_Food",
                c => new
                    {
                        food_id = c.Int(nullable: false),
                        menu_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.food_id, t.menu_id })
                .ForeignKey("dbo.Food", t => t.food_id, cascadeDelete: true)
                .ForeignKey("dbo.Menu", t => t.menu_id, cascadeDelete: true)
                .Index(t => t.food_id)
                .Index(t => t.menu_id);
            
            CreateTable(
                "dbo.Order_has_Food",
                c => new
                    {
                        food_id = c.Int(nullable: false),
                        order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.food_id, t.order_id })
                .ForeignKey("dbo.Food", t => t.food_id, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.order_id, cascadeDelete: true)
                .Index(t => t.food_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.Order_has_Menu",
                c => new
                    {
                        menu_id = c.Int(nullable: false),
                        order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.menu_id, t.order_id })
                .ForeignKey("dbo.Menu", t => t.menu_id, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.order_id, cascadeDelete: true)
                .Index(t => t.menu_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.Menu_has_Drink",
                c => new
                    {
                        drink_id = c.Int(nullable: false),
                        menu_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.drink_id, t.menu_id })
                .ForeignKey("dbo.Drink", t => t.drink_id, cascadeDelete: true)
                .ForeignKey("dbo.Menu", t => t.menu_id, cascadeDelete: true)
                .Index(t => t.drink_id)
                .Index(t => t.menu_id);
            
            CreateTable(
                "dbo.Order_has_Drink",
                c => new
                    {
                        drink_id = c.Int(nullable: false),
                        order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.drink_id, t.order_id })
                .ForeignKey("dbo.Drink", t => t.drink_id, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.order_id, cascadeDelete: true)
                .Index(t => t.drink_id)
                .Index(t => t.order_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order_has_Drink", "order_id", "dbo.Order");
            DropForeignKey("dbo.Order_has_Drink", "drink_id", "dbo.Drink");
            DropForeignKey("dbo.Menu_has_Drink", "menu_id", "dbo.Menu");
            DropForeignKey("dbo.Menu_has_Drink", "drink_id", "dbo.Drink");
            DropForeignKey("dbo.Order_has_Menu", "order_id", "dbo.Order");
            DropForeignKey("dbo.Order_has_Menu", "menu_id", "dbo.Menu");
            DropForeignKey("dbo.Order_has_Food", "order_id", "dbo.Order");
            DropForeignKey("dbo.Order_has_Food", "food_id", "dbo.Food");
            DropForeignKey("dbo.Order", "table_id", "dbo.Table");
            DropForeignKey("dbo.Menu_has_Food", "menu_id", "dbo.Menu");
            DropForeignKey("dbo.Menu_has_Food", "food_id", "dbo.Food");
            DropIndex("dbo.Order_has_Drink", new[] { "order_id" });
            DropIndex("dbo.Order_has_Drink", new[] { "drink_id" });
            DropIndex("dbo.Menu_has_Drink", new[] { "menu_id" });
            DropIndex("dbo.Menu_has_Drink", new[] { "drink_id" });
            DropIndex("dbo.Order_has_Menu", new[] { "order_id" });
            DropIndex("dbo.Order_has_Menu", new[] { "menu_id" });
            DropIndex("dbo.Order_has_Food", new[] { "order_id" });
            DropIndex("dbo.Order_has_Food", new[] { "food_id" });
            DropIndex("dbo.Menu_has_Food", new[] { "menu_id" });
            DropIndex("dbo.Menu_has_Food", new[] { "food_id" });
            DropIndex("dbo.Order", new[] { "table_id" });
            DropTable("dbo.Order_has_Drink");
            DropTable("dbo.Menu_has_Drink");
            DropTable("dbo.Order_has_Menu");
            DropTable("dbo.Order_has_Food");
            DropTable("dbo.Menu_has_Food");
            DropTable("dbo.Table");
            DropTable("dbo.Order");
            DropTable("dbo.Food");
            DropTable("dbo.Menu");
            DropTable("dbo.Drink");
        }
    }
}
