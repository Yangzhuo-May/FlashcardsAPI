using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashcardsAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStackFromCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Stacks_StackId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_StackId",
                table: "Questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Questions_StackId",
                table: "Questions",
                column: "StackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Stacks_StackId",
                table: "Questions",
                column: "StackId",
                principalTable: "Stacks",
                principalColumn: "StackId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
