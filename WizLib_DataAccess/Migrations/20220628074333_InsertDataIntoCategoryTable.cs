using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class InsertDataIntoCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO TBL_CATEGORY VALUES('Category 1')");
            migrationBuilder.Sql("INSERT INTO TBL_CATEGORY VALUES('Category 2')");
            migrationBuilder.Sql("INSERT INTO TBL_CATEGORY VALUES('Category 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
