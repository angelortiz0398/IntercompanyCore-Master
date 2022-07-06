using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntercompanyCore.Migrations
{
    public partial class SegundoContextoDesinger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoOperacion",
                table: "Transaccion",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "Costo",
                table: "Items",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CodBarras",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GesItems",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherAccnt",
                table: "Cuentas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoOperacion",
                table: "Transaccion");

            migrationBuilder.DropColumn(
                name: "CodBarras",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GesItems",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FatherAccnt",
                table: "Cuentas");

            migrationBuilder.AlterColumn<int>(
                name: "Costo",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
