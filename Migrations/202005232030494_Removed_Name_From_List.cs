namespace ShoppingListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_Name_From_List : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShoppingLists", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingLists", "Name", c => c.String(nullable: false));
        }
    }
}
