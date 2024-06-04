namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Move_Tags_toPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Tags", c => c.String());
            DropColumn("dbo.PageComments", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PageComments", "Tags", c => c.String());
            DropColumn("dbo.Pages", "Tags");
        }
    }
}
