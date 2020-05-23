namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ShoppingItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        quantity = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        ShoppingListID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ShoppingCategories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingListID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.ShoppingListID);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingItems", "ShoppingListID", "dbo.ShoppingLists");
            DropForeignKey("dbo.ShoppingItems", "CategoryID", "dbo.ShoppingCategories");
            DropIndex("dbo.ShoppingItems", new[] { "ShoppingListID" });
            DropIndex("dbo.ShoppingItems", new[] { "CategoryID" });
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.ShoppingItems");
            DropTable("dbo.ShoppingCategories");
        }
    }
}
