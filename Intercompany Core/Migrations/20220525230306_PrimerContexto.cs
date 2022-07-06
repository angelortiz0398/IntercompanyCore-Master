using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntercompanyCore.Migrations
{
    public partial class PrimerContexto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentrosCosto",
                columns: table => new
                {
                    Clave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodCentrosCosto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimension = table.Column<int>(type: "int", nullable: false),
                    EsValido = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosCosto", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Clave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RPresupuesto = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Naturaleza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoSAT = table.Column<int>(type: "int", nullable: false),
                    NivelSAT = table.Column<int>(type: "int", nullable: false),
                    DescSAT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuentaOrden = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Clave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grupo = table.Column<int>(type: "int", nullable: false),
                    EsInventario = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    EsVentas = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    EsCompras = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    MetodoInv = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    MetodoCosto = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Costo = table.Column<int>(type: "int", nullable: false),
                    EsValido = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "SocioNegocios",
                columns: table => new
                {
                    Clave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodSocioNegocios = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grupo = table.Column<int>(type: "int", nullable: false),
                    EsValido = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    TipoSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPersona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Serie = table.Column<int>(type: "int", nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuentaDeudor = table.Column<int>(type: "int", nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocioNegocios", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaTransaccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSincronizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoTransaccion = table.Column<int>(type: "int", nullable: false),
                    TipodCRUD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdOrigen = table.Column<int>(type: "int", nullable: false),
                    IdDestino = table.Column<int>(type: "int", nullable: false),
                    Sincronizado = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ErrorDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdObjeto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentrosCosto");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "SocioNegocios");

            migrationBuilder.DropTable(
                name: "Transaccion");
        }
    }
}
