namespace FLYNOBORDERS.SelfB2B.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHotelPictureTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelPictures",
                c => new
                    {
                        HID = c.Int(nullable: false, identity: true),
                        HName = c.String(nullable: false, maxLength: 50),
                        HDesc = c.String(),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HotelPictures");
        }
    }
}
