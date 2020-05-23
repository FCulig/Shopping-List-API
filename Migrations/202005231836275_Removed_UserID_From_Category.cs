namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_UserID_From_Category : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShoppingCategories", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCategories", "UserID", c => c.String(nullable: false));
        }
    }
}
