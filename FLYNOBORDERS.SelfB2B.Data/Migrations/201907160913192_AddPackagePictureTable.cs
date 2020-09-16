namespace FLYNOBORDERS.SelfB2B.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPackagePictureTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackagePictures",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        PName = c.String(nullable: false, maxLength: 50),
                        Price = c.Double(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PackagePictures");
        }
    }
}
