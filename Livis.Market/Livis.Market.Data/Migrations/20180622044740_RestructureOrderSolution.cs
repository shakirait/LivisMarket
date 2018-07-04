using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Livis.Market.Data.Migrations
{
    public partial class RestructureOrderSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartLines");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    MarketId = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderGroups",
                columns: table => new
                {
                    OrderGroupId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<Guid>(nullable: true),
                    BillingCurrency = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    HandlingTotal = table.Column<decimal>(nullable: false),
                    InstanceId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShippingTotal = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    TaxTotal = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGroups", x => x.OrderGroupId);
                    table.ForeignKey(
                        name: "FK_OrderGroups_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderGroups_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderForms",
                columns: table => new
                {
                    OrderFormId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorizedPaymentTotal = table.Column<decimal>(nullable: false),
                    BillingAddressId = table.Column<Guid>(nullable: true),
                    CapturedPaymentTotal = table.Column<decimal>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    HandlingTotal = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OrderGroupId = table.Column<long>(nullable: true),
                    ReturnComment = table.Column<string>(nullable: true),
                    ShippingTotal = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    TaxTotal = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    BillingPartyPartyId = table.Column<Guid>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    EscrowId = table.Column<string>(nullable: true),
                    ExpiresMonth = table.Column<int>(nullable: true),
                    ExpiresYear = table.Column<int>(nullable: true),
                    MaskedNumber = table.Column<string>(nullable: true),
                    PaymentMethodNonce = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    TransactionStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderForms", x => x.OrderFormId);
                    table.ForeignKey(
                        name: "FK_OrderForms_Addresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderForms_OrderGroups_OrderGroupId",
                        column: x => x.OrderGroupId,
                        principalTable: "OrderGroups",
                        principalColumn: "OrderGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderForms_Parties_BillingPartyPartyId",
                        column: x => x.BillingPartyPartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    LineItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    ExtendedPrice = table.Column<decimal>(nullable: false),
                    OrderFormId = table.Column<long>(nullable: true),
                    OrderGroupId = table.Column<long>(nullable: true),
                    PlacedPrice = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    ReturnReason = table.Column<string>(nullable: true),
                    Sku = table.Column<string>(nullable: true),
                    VariantId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.LineItemId);
                    table.ForeignKey(
                        name: "FK_LineItems_OrderForms_OrderFormId",
                        column: x => x.OrderFormId,
                        principalTable: "OrderForms",
                        principalColumn: "OrderFormId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineItems_OrderGroups_OrderGroupId",
                        column: x => x.OrderGroupId,
                        principalTable: "OrderGroups",
                        principalColumn: "OrderGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_OrderFormId",
                table: "LineItems",
                column: "OrderFormId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_OrderGroupId",
                table: "LineItems",
                column: "OrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderForms_BillingAddressId",
                table: "OrderForms",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderForms_OrderGroupId",
                table: "OrderForms",
                column: "OrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderForms_BillingPartyPartyId",
                table: "OrderForms",
                column: "BillingPartyPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGroups_AddressId",
                table: "OrderGroups",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGroups_CustomerId",
                table: "OrderGroups",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "OrderForms");

            migrationBuilder.DropTable(
                name: "OrderGroups");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<Guid>(nullable: false),
                    ItemCount = table.Column<int>(nullable: false),
                    ShopName = table.Column<string>(nullable: true),
                    Totals = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartLines",
                columns: table => new
                {
                    CartLineId = table.Column<Guid>(nullable: false),
                    CartId = table.Column<Guid>(nullable: true),
                    ItemId = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    Totals = table.Column<decimal>(nullable: false),
                    UnitListPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLines", x => x.CartLineId);
                    table.ForeignKey(
                        name: "FK_CartLines_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    BillingPartyPartyId = table.Column<Guid>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    CartId = table.Column<Guid>(nullable: true),
                    EscrowId = table.Column<string>(nullable: true),
                    ExpiresMonth = table.Column<int>(nullable: true),
                    ExpiresYear = table.Column<int>(nullable: true),
                    MaskedNumber = table.Column<string>(nullable: true),
                    PaymentMethodNonce = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    TransactionStatus = table.Column<string>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Parties_BillingPartyPartyId",
                        column: x => x.BillingPartyPartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quality = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    VariantId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartLines_CartId",
                table: "CartLines",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillingPartyPartyId",
                table: "Payments",
                column: "BillingPartyPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CartId",
                table: "Payments",
                column: "CartId");
        }
    }
}
