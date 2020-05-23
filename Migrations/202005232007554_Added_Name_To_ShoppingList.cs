namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Name_To_ShoppingList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingLists", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingLists", "Name");
        }
    }
}
