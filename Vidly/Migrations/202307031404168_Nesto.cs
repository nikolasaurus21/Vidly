namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    //odje sam morao ponovo da dodam migraciju jer sam na proslu prvo kreirao praznu migraciju pa ali sam zaboravio da 
    // dodam u klasu to novo polje ili sam ga slucvajno obrisao zato sam imao error
    public partial class Nesto : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            DropColumn("Movies", "NumberAvailable");
        }
    }
}
