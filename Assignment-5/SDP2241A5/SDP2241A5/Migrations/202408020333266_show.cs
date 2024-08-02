namespace SDP2241A5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class show : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShowBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Genre = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        Coordinator = c.String(),
                        ActorId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShowBaseViewModels");
        }
    }
}
