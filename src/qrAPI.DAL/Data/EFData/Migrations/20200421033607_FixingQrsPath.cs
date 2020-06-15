using Microsoft.EntityFrameworkCore.Migrations;

namespace qrAPI.DAL.Data.EFData.Migrations
{
    public partial class FixingQrsPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Pets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Pets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
