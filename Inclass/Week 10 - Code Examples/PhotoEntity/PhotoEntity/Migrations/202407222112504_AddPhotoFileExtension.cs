namespace PhotoEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoFileExtension : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "FileExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "FileExtension");
        }
    }
}
