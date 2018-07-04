using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Livis.Market.Data.Migrations
{
    public partial class AddOrganisationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationId",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    OrganisationId = table.Column<Guid>(nullable: false),
                    AccountHolder = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    BankNumber = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(nullable: true),
                    BranchNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FaxNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longtitude = table.Column<string>(nullable: true),
                    OpenTime = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Prefecture = table.Column<string>(nullable: true),
                    RegistrationStatus = table.Column<string>(nullable: true),
                    ShopName = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    WebsiteUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.OrganisationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_OrganisationId",
                table: "Contacts",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Organisations_OrganisationId",
                table: "Contacts",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "OrganisationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Organisations_OrganisationId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_OrganisationId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Contacts");
        }
    }
}
