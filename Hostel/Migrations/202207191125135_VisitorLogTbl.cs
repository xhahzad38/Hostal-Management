namespace Hostel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VisitorLogTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisitorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitorName = c.String(nullable: false),
                        VisitorCNIC = c.String(nullable: false),
                        VisitorCardNo = c.String(nullable: false),
                        StudentName = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VisitorLogs");
        }
    }
}
