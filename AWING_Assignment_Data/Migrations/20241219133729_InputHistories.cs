using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AWING_Assignment_Data.Migrations
{
    /// <inheritdoc />
    public partial class InputHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inputHistories",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    n = table.Column<int>(type: "int", nullable: false),
                    m = table.Column<int>(type: "int", nullable: false),
                    p = table.Column<int>(type: "int", nullable: false),
                    matrix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inputHistories", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inputHistories");
        }
    }
}
