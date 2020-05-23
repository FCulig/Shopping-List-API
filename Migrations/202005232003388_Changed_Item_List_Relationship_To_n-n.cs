namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Item_List_Relationship_To_nn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingItems", "ShoppingListID", "dbo.ShoppingLists");
            DropIndex("dbo.ShoppingItems", new[] { "ShoppingListID" });
            CreateTable(
                "dbo.ShoppingListShoppingItems",
                c => new
                    {
                        ShoppingList_ID = c.Int(nullable: false),
                        ShoppingItem_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_ID, t.ShoppingItem_ID })
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_ID, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingItems", t => t.ShoppingItem_ID, cascadeDelete: true)
                .Index(t => t.ShoppingList_ID)
                .Index(t => t.ShoppingItem_ID);
            
            DropColumn("dbo.ShoppingItems", "ShoppingListID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingItems", "ShoppingListID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ShoppingListShoppingItems", "ShoppingItem_ID", "dbo.ShoppingItems");
            DropForeignKey("dbo.ShoppingListShoppingItems", "ShoppingList_ID", "dbo.ShoppingLists");
            DropIndex("dbo.ShoppingListShoppingItems", new[] { "ShoppingItem_ID" });
            DropIndex("dbo.ShoppingListShoppingItems", new[] { "ShoppingList_ID" });
            DropTable("dbo.ShoppingListShoppingItems");
            CreateIndex("dbo.ShoppingItems", "ShoppingListID");
            AddForeignKey("dbo.ShoppingItems", "ShoppingListID", "dbo.ShoppingLists", "ID", cascadeDelete: true);
        }
    }
}
