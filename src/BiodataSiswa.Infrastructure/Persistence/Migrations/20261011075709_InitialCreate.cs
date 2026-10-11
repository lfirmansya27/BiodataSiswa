using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiodataSiswa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_collection_periods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    academic_year = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    starts_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ends_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_collection_periods", x => x.id);
                    table.CheckConstraint("ck_data_collection_periods_dates", "starts_at_utc < ends_at_utc");
                    table.CheckConstraint("ck_data_collection_periods_status", "status IN ('draft','open','closed','finalized')");
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    student_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_data_collection_periods_academic_year_name",
                table: "data_collection_periods",
                columns: new[] { "academic_year", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_student_number",
                table: "students",
                column: "student_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_collection_periods");

            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
