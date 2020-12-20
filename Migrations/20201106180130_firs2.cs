using Microsoft.EntityFrameworkCore.Migrations;

namespace KhaledMohsen.Migrations
{
    public partial class firs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LineCount",
                table: "FileDatas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineCount",
                table: "FileDatas");
        }
    }
}
