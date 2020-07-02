using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Altkom.UniSoft.DbServices.Migrations
{
    public partial class AddSecondNameToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Customers",
                nullable: true);

            migrationBuilder.Sql("UPDATE Customers SET SecondName = 'Test' WHERE SecondName is null");

            migrationBuilder.AlterColumn<string>(
                name: "SecondName",
                table: "Customers",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Customers");
        }
    }
}
