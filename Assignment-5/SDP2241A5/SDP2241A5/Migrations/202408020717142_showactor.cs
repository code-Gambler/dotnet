namespace SDP2241A5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class showactor : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EpisodeWithShowNameViewModels", newName: "EpisodeBaseViewModels");
            AddColumn("dbo.ActorBaseViewModels", "ShowWithInfoViewModel_Id", c => c.Int());
            AddColumn("dbo.EpisodeBaseViewModels", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.EpisodeBaseViewModels", "ShowWithInfoViewModel_Id", c => c.Int());
            AddColumn("dbo.ShowBaseViewModels", "ActorsCount", c => c.Int());
            AddColumn("dbo.ShowBaseViewModels", "EpisodesCount", c => c.Int());
            AddColumn("dbo.ShowBaseViewModels", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ActorBaseViewModels", "ShowWithInfoViewModel_Id");
            CreateIndex("dbo.EpisodeBaseViewModels", "ShowWithInfoViewModel_Id");
            AddForeignKey("dbo.ActorBaseViewModels", "ShowWithInfoViewModel_Id", "dbo.ShowBaseViewModels", "Id");
            AddForeignKey("dbo.EpisodeBaseViewModels", "ShowWithInfoViewModel_Id", "dbo.ShowBaseViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EpisodeBaseViewModels", "ShowWithInfoViewModel_Id", "dbo.ShowBaseViewModels");
            DropForeignKey("dbo.ActorBaseViewModels", "ShowWithInfoViewModel_Id", "dbo.ShowBaseViewModels");
            DropIndex("dbo.EpisodeBaseViewModels", new[] { "ShowWithInfoViewModel_Id" });
            DropIndex("dbo.ActorBaseViewModels", new[] { "ShowWithInfoViewModel_Id" });
            DropColumn("dbo.ShowBaseViewModels", "Discriminator");
            DropColumn("dbo.ShowBaseViewModels", "EpisodesCount");
            DropColumn("dbo.ShowBaseViewModels", "ActorsCount");
            DropColumn("dbo.EpisodeBaseViewModels", "ShowWithInfoViewModel_Id");
            DropColumn("dbo.EpisodeBaseViewModels", "Discriminator");
            DropColumn("dbo.ActorBaseViewModels", "ShowWithInfoViewModel_Id");
            RenameTable(name: "dbo.EpisodeBaseViewModels", newName: "EpisodeWithShowNameViewModels");
        }
    }
}
