namespace FLYNOBORDERS.SelfB2B.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyInfoes",
                c => new
                    {
                        CID = c.Int(nullable: false, identity: true),
                        CName = c.String(nullable: false, maxLength: 250),
                        TradeLicenseNo = c.String(nullable: false),
                        Website = c.String(nullable: false, maxLength: 150),
                        CEmail = c.String(nullable: false),
                        PhnNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false, maxLength: 150),
                        Zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CID);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UEmail = c.String(nullable: false),
                        Pass = c.String(nullable: false),
                        TimeZone = c.String(nullable: false),
                        PreferredRoe = c.String(nullable: false),
                        UserTypeID = c.Int(nullable: false),
                        AdminVerifyID = c.Int(nullable: false),
                        OTP = c.String(),
                        EmailVerifyId = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UID)
                .ForeignKey("dbo.CompanyInfoes", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "CompanyID", "dbo.CompanyInfoes");
            DropIndex("dbo.UserInfoes", new[] { "CompanyID" });
            DropTable("dbo.UserInfoes");
            DropTable("dbo.CompanyInfoes");
        }
    }
}
