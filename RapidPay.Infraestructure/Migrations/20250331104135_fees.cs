using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPay.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class fees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fees",
                schema: "rapidpay",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fees", x => x.id);
                });

            migrationBuilder.Sql($@"
                INSERT INTO rapidpay.fees (id, value, created_at, updated_at)
                VALUES (
                    '{Guid.NewGuid()}',
                    2.0,
                    timezone('utc', now()),
                    timezone('utc', now())
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fees",
                schema: "rapidpay");
        }
    }
}
