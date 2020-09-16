namespace FLYNOBORDERS.SelfB2B.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringToIntRoeAndTimeZone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "TimeZone", c => c.Int(nullable: false));
            AlterColumn("dbo.UserInfoes", "PreferredRoe", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "PreferredRoe", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "TimeZone", c => c.String(nullable: false));
        }
    }
}
