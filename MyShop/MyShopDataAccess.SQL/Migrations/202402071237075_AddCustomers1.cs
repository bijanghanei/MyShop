namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomers1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Email", c => c.String());
            DropColumn("dbo.Customers", "Emial");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Emial", c => c.String());
            DropColumn("dbo.Customers", "Email");
        }
    }
}
