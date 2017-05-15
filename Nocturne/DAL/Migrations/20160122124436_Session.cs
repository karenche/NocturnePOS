using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Nocturne.DAL.Migrations
{
    public partial class Session : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Discount_DiscountType_DiscountTypeId", table: "Discount");
            migrationBuilder.DropForeignKey(name: "FK_Discount_Product_ProductId", table: "Discount");
            migrationBuilder.DropForeignKey(name: "FK_UserRole_Role_RoleId", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_UserRole_User_UserId", table: "UserRole");
            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    RegisteredById = table.Column<int>(nullable: false),
                    To = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Session_User_RegisteredById",
                        column: x => x.RegisteredById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "UsedProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    RegisteredById = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsedProduct_User_RegisteredById",
                        column: x => x.RegisteredById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsedProduct_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_Discount_DiscountType_DiscountTypeId",
                table: "Discount",
                column: "DiscountTypeId",
                principalTable: "DiscountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Product_ProductId",
                table: "Discount",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Discount_DiscountType_DiscountTypeId", table: "Discount");
            migrationBuilder.DropForeignKey(name: "FK_Discount_Product_ProductId", table: "Discount");
            migrationBuilder.DropForeignKey(name: "FK_UserRole_Role_RoleId", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_UserRole_User_UserId", table: "UserRole");
            migrationBuilder.DropTable("UsedProduct");
            migrationBuilder.DropTable("Session");
            migrationBuilder.AddForeignKey(
                name: "FK_Discount_DiscountType_DiscountTypeId",
                table: "Discount",
                column: "DiscountTypeId",
                principalTable: "DiscountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Product_ProductId",
                table: "Discount",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
