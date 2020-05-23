namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Bought_To_Item : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingListShoppingItems", "ShoppingList_ID", "dbo.ShoppingLists");
            DropForeignKey("dbo.ShoppingListShoppingItems", "ShoppingItem_ID", "dbo.ShoppingItems");
            DropIndex("dbo.ShoppingListShoppingItems", new[] { "ShoppingList_ID" });
            DropIndex("dbo.ShoppingListShoppingItems", new[] { "ShoppingItem_ID" });
            AddColumn("dbo.ShoppingItems", "Bought", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShoppingItems", "ListID", c => c.Int(nullable: false));
            CreateIndex("dbo.ShoppingItems", "ListID");
            AddForeignKey("dbo.ShoppingItems", "ListID", "dbo.ShoppingLists", "ID", cascadeDelete: true);
            DropTable("dbo.ShoppingListShoppingItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShoppingListShoppingItems",
                c => new
                    {
                        ShoppingList_ID = c.Int(nullable: false),
                        ShoppingItem_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_ID, t.ShoppingItem_ID });
            
            DropForeignKey("dbo.ShoppingItems", "ListID", "dbo.ShoppingLists");
            DropIndex("dbo.ShoppingItems", new[] { "ListID" });
            DropColumn("dbo.ShoppingItems", "ListID");
            DropColumn("dbo.ShoppingItems", "Bought");
            CreateIndex("dbo.ShoppingListShoppingItems", "ShoppingItem_ID");
            CreateIndex("dbo.ShoppingListShoppingItems", "ShoppingList_ID");
            AddForeignKey("dbo.ShoppingListShoppingItems", "ShoppingItem_ID", "dbo.ShoppingItems", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ShoppingListShoppingItems", "ShoppingList_ID", "dbo.ShoppingLists", "ID", cascadeDelete: true);
        }
    }
}
