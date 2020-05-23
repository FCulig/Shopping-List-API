namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserIDs_To_Tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCategories", "UserID", c => c.String(nullable: false));
            AddColumn("dbo.ShoppingItems", "UserID", c => c.String(nullable: false));
            AlterColumn("dbo.ShoppingItems", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingItems", "Name", c => c.String());
            DropColumn("dbo.ShoppingItems", "UserID");
            DropColumn("dbo.ShoppingCategories", "UserID");
        }
    }
}
