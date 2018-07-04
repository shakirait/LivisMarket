using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Livis.Market.Data.Migrations
{
    public partial class AppendEmailFieldsToOrganisationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivateAccount",
                table: "Organisations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSendConfirmationMail",
                table: "Organisations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSendThankYouMail",
                table: "Organisations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TokenConfirmationMail",
                table: "Organisations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivateAccount",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "IsSendConfirmationMail",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "IsSendThankYouMail",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "TokenConfirmationMail",
                table: "Organisations");
        }
    }
}
