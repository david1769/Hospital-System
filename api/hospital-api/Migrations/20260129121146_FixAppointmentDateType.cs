using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hospital_api.Migrations
{
    /// <inheritdoc />
    public partial class FixAppointmentDateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ReferenceDataCategory",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ReferenceDataCategory",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ReferenceDataCategory",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ReferenceDataCategory",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ReferenceDataCategory",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ReferenceDataCategory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ReferenceDataCategory");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ReferenceDataCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ReferenceDataCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ReferenceDataCategory");
        }
    }
}
