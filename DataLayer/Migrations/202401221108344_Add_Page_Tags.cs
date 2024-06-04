namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Page_Tags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageComments", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageComments", "Tags");
        }
    }
}
