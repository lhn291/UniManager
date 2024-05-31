using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_lecturers",
                columns: table => new
                {
                    LecturerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Lecturer")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_lecturers", x => x.LecturerId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Student"),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_courses",
                columns: table => new
                {
                    CourseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_tbl_courses_tbl_lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "tbl_lecturers",
                        principalColumn: "LecturerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_course_students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_course_students", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_tbl_course_students_tbl_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tbl_courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_course_students_tbl_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tbl_students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_course_students_CourseId",
                table: "tbl_course_students",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_courses_CourseID",
                table: "tbl_courses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_courses_LecturerId",
                table: "tbl_courses",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_lecturers_Email",
                table: "tbl_lecturers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_students_Email",
                table: "tbl_students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_students_PhoneNumber",
                table: "tbl_students",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_admins");

            migrationBuilder.DropTable(
                name: "tbl_course_students");

            migrationBuilder.DropTable(
                name: "tbl_courses");

            migrationBuilder.DropTable(
                name: "tbl_students");

            migrationBuilder.DropTable(
                name: "tbl_lecturers");
        }
    }
}
