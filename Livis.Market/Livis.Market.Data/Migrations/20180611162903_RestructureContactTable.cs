using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Livis.Market.Data.Migrations
{
    public partial class RestructureContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MobileMailAddress",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerGroup",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "ModifierId",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerGroup",
                table: "Contacts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileMailAddress",
                table: "Contacts",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifierId",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Addresses",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Addresses",
                maxLength: 64,
                nullable: true);
        }
    }
}
