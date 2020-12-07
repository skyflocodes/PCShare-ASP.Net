using Microsoft.EntityFrameworkCore.Migrations;

namespace PCShare.Data.Migrations
{
    public partial class migration23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pcs",
                table: "Pcs");

            migrationBuilder.RenameTable(
                name: "Pcs",
                newName: "PC");

            migrationBuilder.RenameIndex(
                name: "IX_Pcs_UserId",
                table: "PC",
                newName: "IX_PC_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PC",
                table: "PC",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PC",
                table: "PC");

            migrationBuilder.RenameTable(
                name: "PC",
                newName: "Pcs");

            migrationBuilder.RenameIndex(
                name: "IX_PC_UserId",
                table: "Pcs",
                newName: "IX_Pcs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pcs",
                table: "Pcs",
                column: "Id");
        }
    }
}
