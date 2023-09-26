using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBFirstLibrary.Migrations
{
    public partial class AddOptimisticConcurrencyTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "books",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "authors",
                type: "longblob",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "books");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "authors");
        }
    }
}
