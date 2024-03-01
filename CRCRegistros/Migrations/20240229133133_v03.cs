using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRCRegistros.Migrations
{
    public partial class v03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimo_Items_NameId",
                table: "Emprestimo");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimo_NameId",
                table: "Emprestimo");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "NameId",
                table: "Emprestimo",
                newName: "CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Emprestimo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Emprestimo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Emprestimo");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Emprestimo");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Emprestimo",
                newName: "NameId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimo_NameId",
                table: "Emprestimo",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimo_Items_NameId",
                table: "Emprestimo",
                column: "NameId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
