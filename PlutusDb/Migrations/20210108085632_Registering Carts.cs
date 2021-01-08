using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlutusDb.Migrations
{
    public partial class RegisteringCarts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartExpenses_Clients_ClientId",
                table: "CartExpenses");

            migrationBuilder.DropTable(
                name: "HistoryElements");

            migrationBuilder.DropTable(
                name: "ShoppingExpenses");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "CartExpenses",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "CartExpenses",
                newName: "State");

            migrationBuilder.RenameIndex(
                name: "IX_CartExpenses_ClientId",
                table: "CartExpenses",
                newName: "IX_CartExpenses_CartId");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ClientId",
                table: "Carts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartExpenses_Carts_CartId",
                table: "CartExpenses",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartExpenses_Carts_CartId",
                table: "CartExpenses");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "CartExpenses",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartExpenses",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_CartExpenses_CartId",
                table: "CartExpenses",
                newName: "IX_CartExpenses_ClientId");

            migrationBuilder.CreateTable(
                name: "HistoryElements",
                columns: table => new
                {
                    HistoryElementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryElements", x => x.HistoryElementId);
                    table.ForeignKey(
                        name: "FK_HistoryElements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingExpenses",
                columns: table => new
                {
                    ShoppingExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingExpenses", x => x.ShoppingExpenseId);
                    table.ForeignKey(
                        name: "FK_ShoppingExpenses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryElements_ClientId",
                table: "HistoryElements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingExpenses_ClientId",
                table: "ShoppingExpenses",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartExpenses_Clients_ClientId",
                table: "CartExpenses",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
