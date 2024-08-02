namespace SDP2241A5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actorshow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EpisodeWithShowNameViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShowName = c.String(),
                        Name = c.String(),
                        SeasonNumber = c.Int(nullable: false),
                        EpisodeNumber = c.Int(nullable: false),
                        Genre = c.String(),
                        AirDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        Clerk = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ActorBaseViewModels", "ShowsCount", c => c.Int());
            AddColumn("dbo.ActorBaseViewModels", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActorBaseViewModels", "Discriminator");
            DropColumn("dbo.ActorBaseViewModels", "ShowsCount");
            DropTable("dbo.EpisodeWithShowNameViewModels");
        }
    }
}
