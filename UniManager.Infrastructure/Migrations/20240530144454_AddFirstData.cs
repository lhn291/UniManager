using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_courses",
                columns: table => new
                {
                    CourseID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_courses", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_lecturers",
                columns: table => new
                {
                    LecturerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_lecturers", x => x.LecturerId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CourseID = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_tbl_students_tbl_courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "tbl_courses",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_subjects",
                columns: table => new
                {
                    SubjectID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LecturerId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_subjects", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_tbl_subjects_tbl_lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "tbl_lecturers",
                        principalColumn: "LecturerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_course_subjects",
                columns: table => new
                {
                    CourseID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_course_subjects", x => new { x.CourseID, x.SubjectID });
                    table.ForeignKey(
                        name: "FK_tbl_course_subjects_tbl_courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "tbl_courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_course_subjects_tbl_subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "tbl_subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_exams",
                columns: table => new
                {
                    ExamID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExamName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_exams", x => x.ExamID);
                    table.ForeignKey(
                        name: "FK_tbl_exams_tbl_subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "tbl_subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_exam_scores",
                columns: table => new
                {
                    ScoreID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_exam_scores", x => x.ScoreID);
                    table.ForeignKey(
                        name: "FK_tbl_exam_scores_tbl_exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "tbl_exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_exam_scores_tbl_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tbl_students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_course_subjects_SubjectID",
                table: "tbl_course_subjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_exam_scores_ExamID",
                table: "tbl_exam_scores",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_exam_scores_StudentId",
                table: "tbl_exam_scores",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_exams_SubjectID",
                table: "tbl_exams",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_students_CourseID",
                table: "tbl_students",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_subjects_LecturerId",
                table: "tbl_subjects",
                column: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_admins");

            migrationBuilder.DropTable(
                name: "tbl_course_subjects");

            migrationBuilder.DropTable(
                name: "tbl_exam_scores");

            migrationBuilder.DropTable(
                name: "tbl_exams");

            migrationBuilder.DropTable(
                name: "tbl_students");

            migrationBuilder.DropTable(
                name: "tbl_subjects");

            migrationBuilder.DropTable(
                name: "tbl_courses");

            migrationBuilder.DropTable(
                name: "tbl_lecturers");
        }
    }
}
