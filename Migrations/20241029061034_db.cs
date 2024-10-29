using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentsApi.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdCard = table.Column<long>(type: "bigint", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdCard);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    DepositID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DepositFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIdCard = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.DepositID);
                    table.ForeignKey(
                        name: "FK_Deposit_User_UserIdCard",
                        column: x => x.UserIdCard,
                        principalTable: "User",
                        principalColumn: "IdCard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    TransferID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransferFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferFlagID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UserIdCard = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.TransferID);
                    table.ForeignKey(
                        name: "FK_Transfer_User_UserIdCard",
                        column: x => x.UserIdCard,
                        principalTable: "User",
                        principalColumn: "IdCard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdraw",
                columns: table => new
                {
                    WithdrawID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WithdrawAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithdrawStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WithdrawDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    WithdrawFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIdCard = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraw", x => x.WithdrawID);
                    table.ForeignKey(
                        name: "FK_Withdraw_User_UserIdCard",
                        column: x => x.UserIdCard,
                        principalTable: "User",
                        principalColumn: "IdCard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_UserIdCard",
                table: "Deposit",
                column: "UserIdCard");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_UserIdCard",
                table: "Transfer",
                column: "UserIdCard");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraw_UserIdCard",
                table: "Withdraw",
                column: "UserIdCard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "Withdraw");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
