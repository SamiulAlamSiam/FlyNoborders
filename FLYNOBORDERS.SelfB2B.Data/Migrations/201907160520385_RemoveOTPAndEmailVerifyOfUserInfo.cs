namespace FLYNOBORDERS.SelfB2B.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOTPAndEmailVerifyOfUserInfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserInfoes", "OTP");
            DropColumn("dbo.UserInfoes", "EmailVerifyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "EmailVerifyId", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "OTP", c => c.String());
        }
    }
}
