using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProject.Migrations
{
    /// <inheritdoc />
    public partial class TestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompInProjects");

            migrationBuilder.CreateTable(
                name: "CompetenceProject",
                columns: table => new
                {
                    CompetencesCompetenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectsProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceProject", x => new { x.CompetencesCompetenceId, x.ProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_CompetenceProject_Competences_CompetencesCompetenceId",
                        column: x => x.CompetencesCompetenceId,
                        principalTable: "Competences",
                        principalColumn: "CompetenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenceProject_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceProject_ProjectsProjectId",
                table: "CompetenceProject",
                column: "ProjectsProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetenceProject");

            migrationBuilder.CreateTable(
                name: "CompInProjects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompInProjects", x => new { x.ProjectId, x.CompetenceId });
                    table.ForeignKey(
                        name: "FK_CompInProjects_Competences_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competences",
                        principalColumn: "CompetenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompInProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompInProjects_CompetenceId",
                table: "CompInProjects",
                column: "CompetenceId");
        }
    }
}
