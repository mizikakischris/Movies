using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Repository.Migrations
{
    public partial class RenameHeroTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hero_Actors_ActorId",
                table: "Hero");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hero",
                table: "Hero");

            migrationBuilder.RenameTable(
                name: "Hero",
                newName: "Heroes");

            migrationBuilder.RenameIndex(
                name: "IX_Hero_ActorId",
                table: "Heroes",
                newName: "IX_Heroes_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Heroes",
                table: "Heroes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Actors_ActorId",
                table: "Heroes",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Actors_ActorId",
                table: "Heroes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Heroes",
                table: "Heroes");

            migrationBuilder.RenameTable(
                name: "Heroes",
                newName: "Hero");

            migrationBuilder.RenameIndex(
                name: "IX_Heroes_ActorId",
                table: "Hero",
                newName: "IX_Hero_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hero",
                table: "Hero",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hero_Actors_ActorId",
                table: "Hero",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
