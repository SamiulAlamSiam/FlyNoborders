namespace FLYNOBORDERS.SelfB2B.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepositeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deposites",
                c => new
                    {
                        UID = c.Int(nullable: false, identity: true),
                        DType = c.String(nullable: false),
                        ChequeNo = c.String(nullable: false),
                        RefNum = c.String(nullable: false),
                        ChequeBank = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        BankId = c.Int(nullable: false),
                        ReceiptImage = c.Binary(nullable: false),
                        AdminVerifyId = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UID)
                .ForeignKey("dbo.UserInfoes", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deposites", "UserID", "dbo.UserInfoes");
            DropIndex("dbo.Deposites", new[] { "UserID" });
            DropTable("dbo.Deposites");
        }
    }
}
