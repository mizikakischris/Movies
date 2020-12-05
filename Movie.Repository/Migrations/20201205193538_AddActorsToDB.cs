using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Repository.Migrations
{
    public partial class AddActorsToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Actor_MovieModel",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieModel",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "MovieModel",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Actors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actors",
                table: "Actors",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.RenameTable(
                name: "Actors",
                newName: "Actor");

            migrationBuilder.AddColumn<int>(
                name: "MovieModel",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieModel",
                table: "Movies",
                column: "MovieModel");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Actor_MovieModel",
                table: "Movies",
                column: "MovieModel",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
