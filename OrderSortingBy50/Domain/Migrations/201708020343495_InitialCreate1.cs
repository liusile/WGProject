namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            Sql("insert into SysConfig(LatticeLineCount,PrintType) values(10,'0')");
            Sql("update SysConfig set IP='192.168.1.71' , Port='8088'");
        }
        
        public override void Down()
        {
        }
    }
}
