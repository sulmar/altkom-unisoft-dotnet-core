using Microsoft.EntityFrameworkCore.Migrations;

namespace Altkom.UniSoft.DbServices.Migrations
{
    public partial class AdduspGetCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW vwCustomers AS SELECT * FROM Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vwCustomers");
        }
    }
}
