namespace rzeczuchyTrack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTimeEntry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeEntries");
        }
    }
}
