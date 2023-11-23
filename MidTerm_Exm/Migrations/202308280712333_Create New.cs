namespace MidTerm_Exm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false, maxLength: 40),
                        Capital = c.String(nullable: false, maxLength: 30),
                        Currency = c.String(nullable: false, maxLength: 50),
                        Symbol = c.String(nullable: false, maxLength: 50),
                        Picture = c.String(nullable: false, maxLength: 50),
                        IsDeveloped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniversityName = c.String(nullable: false, maxLength: 30),
                        AdmissionRequirment = c.String(nullable: false, maxLength: 40),
                        EstublishDate = c.DateTime(nullable: false, storeType: "date"),
                        PayingCost = c.Decimal(nullable: false, storeType: "money"),
                        Ranking = c.String(nullable: false, maxLength: 30),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            CreateStoredProcedure("dbo.DeleteCountry",
                c => new
                {
                    id = c.Int()
                }, @"DELETE FROM Countries WHERE Id = @id");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Universities", "CountryId", "dbo.Countries");
            DropIndex("dbo.Universities", new[] { "CountryId" });
            DropTable("dbo.Universities");
            DropTable("dbo.Countries");
            DropStoredProcedure("dbo.DeleteCountry");
        }
    }
}
