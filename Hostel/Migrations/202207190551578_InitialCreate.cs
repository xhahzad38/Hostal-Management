namespace Hostel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        BilledDate = c.DateTime(nullable: false),
                        BillDetails = c.String(nullable: false),
                        Ammount = c.Single(nullable: false),
                        SubmitBy = c.String(),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        BuildingName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BuildingId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 12),
                        Address = c.String(nullable: false),
                        AppointDate = c.DateTime(nullable: false),
                        BuildingId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.BuildingId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.EmployeeSalaries",
                c => new
                    {
                        SalaryId = c.Int(nullable: false, identity: true),
                        PayDate = c.DateTime(nullable: false),
                        Details = c.String(nullable: false),
                        AmmountPaid = c.Single(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaryId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(nullable: false),
                        Salary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PositionId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        PurchaseDate = c.DateTime(nullable: false),
                        Weight = c.Single(nullable: false),
                        Price = c.Single(nullable: false),
                        ProductId = c.Int(nullable: false),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Unit = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(nullable: false),
                        TotalSeat = c.Int(nullable: false),
                        BuildingId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.RoomCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.BuildingId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 12),
                        Address = c.String(nullable: false),
                        Photo = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.MemberPayments",
                c => new
                    {
                        PayId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Details = c.String(nullable: false),
                        Debit = c.Single(nullable: false),
                        Credit = c.Single(nullable: false),
                        Balance = c.Single(nullable: false),
                        MemberId = c.Int(nullable: false),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.MessCharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Charges = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.RoomCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        RoomType = c.String(nullable: false),
                        Rent = c.Int(nullable: false),
                        Facility = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Hostels",
                c => new
                    {
                        HostelId = c.Int(nullable: false, identity: true),
                        HostelName = c.String(nullable: false),
                        HostelTitle = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 12),
                        City = c.String(nullable: false),
                        Area = c.String(nullable: false),
                        Road = c.String(nullable: false),
                        House = c.String(nullable: false),
                        HostelLogo = c.String(),
                    })
                .PrimaryKey(t => t.HostelId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "CategoryId", "dbo.RoomCategories");
            DropForeignKey("dbo.Members", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.MessCharges", "MemberId", "dbo.Members");
            DropForeignKey("dbo.MemberPayments", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Purchases", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Purchases", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.EmployeeSalaries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Bills", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.MessCharges", new[] { "MemberId" });
            DropIndex("dbo.MemberPayments", new[] { "MemberId" });
            DropIndex("dbo.Members", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "CategoryId" });
            DropIndex("dbo.Rooms", new[] { "BuildingId" });
            DropIndex("dbo.Purchases", new[] { "BuildingId" });
            DropIndex("dbo.Purchases", new[] { "ProductId" });
            DropIndex("dbo.EmployeeSalaries", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "BuildingId" });
            DropIndex("dbo.Bills", new[] { "BuildingId" });
            DropTable("dbo.Users");
            DropTable("dbo.Hostels");
            DropTable("dbo.RoomCategories");
            DropTable("dbo.MessCharges");
            DropTable("dbo.MemberPayments");
            DropTable("dbo.Members");
            DropTable("dbo.Rooms");
            DropTable("dbo.Products");
            DropTable("dbo.Purchases");
            DropTable("dbo.Positions");
            DropTable("dbo.EmployeeSalaries");
            DropTable("dbo.Employees");
            DropTable("dbo.Buildings");
            DropTable("dbo.Bills");
        }
    }
}
