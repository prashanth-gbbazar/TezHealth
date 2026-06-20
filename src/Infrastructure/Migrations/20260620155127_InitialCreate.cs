using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TezHealth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drugs",
                columns: table => new
                {
                    DrugId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "uuid", nullable: true),
                    DrugName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    GenericName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Strength = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MinimumStockThreshold = table.Column<int>(type: "integer", nullable: true),
                    StorageCondition = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drugs", x => x.DrugId);
                });

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    VendorName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContactPerson = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Pincode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    GstNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    DrugLicenseNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PaymentTerms = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendors", x => x.VendorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_drugs_CategoryId",
                table: "drugs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_drugs_DrugId",
                table: "drugs",
                column: "DrugId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vendors_City",
                table: "vendors",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_Email",
                table: "vendors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vendors_PhoneNumber",
                table: "vendors",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_State",
                table: "vendors",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_VendorId",
                table: "vendors",
                column: "VendorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "drugs");

            migrationBuilder.DropTable(
                name: "vendors");
        }
    }
}
