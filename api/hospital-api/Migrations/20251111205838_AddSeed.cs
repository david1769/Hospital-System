using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hospital_api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "CreatedAt", "CreatedBy", "DateOfBirth", "Email", "EmergencyContact", "FirstName", "Gender", "HealthInsuranceNumber", "InsuranceProviderId", "IsActive", "LastName", "MedicalHistory", "NationalIdNumber", "PhoneNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "Washington Street Akasia", null, null, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 38388832, "John", true, null, null, null, "Doe", null, null, 12334944L, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
