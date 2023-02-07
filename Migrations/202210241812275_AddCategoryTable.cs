namespace Job_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CategoryName = c.String(nullable: false),
                    CategoryDescription = c.String(nullable: false),
                }).PrimaryKey(p => p.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
