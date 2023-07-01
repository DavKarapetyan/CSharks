using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharks.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addQuizScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionAnswerId = table.Column<int>(type: "int", nullable: false),
                    QuizTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizScores_QuestionAnswers_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuizScores_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuizScores_QuizTypes_QuizTypeId",
                        column: x => x.QuizTypeId,
                        principalTable: "QuizTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizScores_QuestionAnswerId",
                table: "QuizScores",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizScores_QuestionId",
                table: "QuizScores",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizScores_QuizTypeId",
                table: "QuizScores",
                column: "QuizTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizScores");
        }
    }
}
