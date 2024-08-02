namespace SDP2241A5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActorBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AlternateName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(),
                        Executive = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActorBaseViewModels");
        }
    }
}
