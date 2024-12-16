using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SumCalculator.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calculation_records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value1 = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    value2 = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    sum = table.Column<decimal>(type: "decimal(18,6)", nullable: false, computedColumnSql: "[value1] + [value2]", stored: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calculation_records", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculation_records");
        }
    }
}
