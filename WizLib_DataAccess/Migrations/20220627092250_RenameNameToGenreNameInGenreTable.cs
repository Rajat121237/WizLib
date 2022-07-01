using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class RenameNameToGenreNameInGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "Genres",
                nullable: true);

            migrationBuilder.Sql("UPDATE DBO.GENRES SET GENRENAME = NAME");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
               name: "Name",
               table: "Genres",
               type: "nvarchar(max)",
               nullable: true);

            migrationBuilder.Sql("UPDATE DBO.GENRES SET NAME = GENRENAME");

            migrationBuilder.DropColumn(
                name: "GenreName",
                table: "Genres");
        }
    }
}
