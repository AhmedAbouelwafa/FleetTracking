using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FleetTracking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertToPostgreSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlateNumber = table.Column<string>(type: "text", nullable: false),
                    DriverName = table.Column<string>(type: "text", nullable: false),
                    LastLatitude = table.Column<double>(type: "double precision", nullable: true),
                    LastLongitude = table.Column<double>(type: "double precision", nullable: true),
                    LastSpeed = table.Column<double>(type: "double precision", nullable: true),
                    LastHeading = table.Column<double>(type: "double precision", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CreatedAt", "DriverName", "IsActive", "IsDeleted", "LastHeading", "LastLatitude", "LastLongitude", "LastSpeed", "LastUpdatedAt", "PlateNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "أحمد محمد", true, false, 90.0, 30.0444, 31.235700000000001, 65.5, new DateTime(2025, 12, 31, 23, 58, 0, 0, DateTimeKind.Utc), "أ ب ج 1234", 1, new DateTime(2025, 12, 31, 23, 58, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "محمود علي", true, false, 45.0, 30.0626, 31.354700000000001, 0.0, new DateTime(2025, 12, 31, 23, 57, 0, 0, DateTimeKind.Utc), "د هـ و 5678", 2, new DateTime(2025, 12, 31, 23, 57, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "خالد حسن", true, false, 180.0, 31.200099999999999, 29.918700000000001, 0.0, new DateTime(2025, 12, 31, 23, 52, 0, 0, DateTimeKind.Utc), "ز ح ط 9012", 3, new DateTime(2025, 12, 31, 23, 52, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), "عمر سعيد", true, false, 270.0, 26.820599999999999, 30.802499999999998, 0.0, new DateTime(2025, 12, 31, 23, 45, 0, 0, DateTimeKind.Utc), "ي ك ل 3456", 4, new DateTime(2025, 12, 31, 23, 45, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Utc), "يوسف إبراهيم", true, false, 315.0, 30.5852, 30.9922, 110.0, new DateTime(2025, 12, 31, 23, 59, 0, 0, DateTimeKind.Utc), "م ن س 7890", 1, new DateTime(2025, 12, 31, 23, 59, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2025, 12, 31, 22, 0, 0, 0, DateTimeKind.Utc), "حسن عبدالله", true, false, null, null, null, null, null, "ع ف ص 2468", 4, new DateTime(2025, 12, 31, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), "مصطفى أحمد", true, false, 0.0, 25.687200000000001, 32.639600000000002, 45.0, new DateTime(2025, 12, 31, 23, 55, 0, 0, DateTimeKind.Utc), "ق ر ش 1357", 1, new DateTime(2025, 12, 31, 23, 55, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "سامي وليد", false, false, 0.0, 30.0444, 31.235700000000001, 0.0, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "ت ث خ 9753", 4, new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), "طارق فتحي", true, false, 270.0, 31.040900000000001, 28.9617, 75.0, new DateTime(2025, 12, 31, 23, 59, 30, 0, DateTimeKind.Utc), "ذ ض ظ 8642", 1, new DateTime(2025, 12, 31, 23, 59, 30, 0, DateTimeKind.Utc) },
                    { 10, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), "وليد صلاح", true, false, 135.0, 30.583300000000001, 32.2667, 55.0, new DateTime(2025, 12, 31, 23, 56, 0, 0, DateTimeKind.Utc), "غ إ أ 7531", 1, new DateTime(2025, 12, 31, 23, 56, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_IsDeleted",
                table: "Vehicles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LastUpdatedAt",
                table: "Vehicles",
                column: "LastUpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Status",
                table: "Vehicles",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
