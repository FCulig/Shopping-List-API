namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_Required_For_UserIDs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShoppingItems", "UserID", c => c.String());
            AlterColumn("dbo.ShoppingLists", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingLists", "UserID", c => c.String(nullable: false));
            AlterColumn("dbo.ShoppingItems", "UserID", c => c.String(nullable: false));
        }
    }
}
