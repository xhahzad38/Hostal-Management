namespace Hostel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessDishesTblCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessDishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DishName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MessDishes");
        }
    }
}
