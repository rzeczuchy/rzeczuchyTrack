namespace rzeczuchyTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyingTimeData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeEntries", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.TimeEntries", "Minutes", c => c.Int(nullable: false));
            AddColumn("dbo.TimeEntries", "Seconds", c => c.Int(nullable: false));
            DropColumn("dbo.TimeEntries", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeEntries", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.TimeEntries", "Seconds");
            DropColumn("dbo.TimeEntries", "Minutes");
            DropColumn("dbo.TimeEntries", "Hours");
        }
    }
}
