namespace rzeczuchyTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingConstraints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeEntries", "TrackedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeEntries", "Label", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeEntries", "Label", c => c.String());
            DropColumn("dbo.TimeEntries", "TrackedOn");
        }
    }
}
