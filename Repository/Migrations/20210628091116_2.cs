using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Titel",
                table: "Comments",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CommentTekst",
                table: "Comments",
                newName: "CommentText");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Comments",
                newName: "Titel");

            migrationBuilder.RenameColumn(
                name: "CommentText",
                table: "Comments",
                newName: "CommentTekst");
        }
    }
}
