using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaLegos.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

           

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apelido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    País = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrTelemovel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrContribuinte = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gestor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrTelemovel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestor", x => x.Id);
                });

           

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encomendas_Clientes_ClienteFK",
                        column: x => x.ClienteFK,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrTelemovel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChefeFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Gestor_ChefeFK",
                        column: x => x.ChefeFK,
                        principalTable: "Gestor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Armazem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armazem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Armazem_Funcionarios_ResponsavelFK",
                        column: x => x.ResponsavelFK,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrPecas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalhes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArmazemFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artigos_Armazem_ArmazemFK",
                        column: x => x.ArmazemFK,
                        principalTable: "Armazem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtigoEncomenda",
                columns: table => new
                {
                    ArtigosId = table.Column<int>(type: "int", nullable: false),
                    EncomendasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigoEncomenda", x => new { x.ArtigosId, x.EncomendasId });
                    table.ForeignKey(
                        name: "FK_ArtigoEncomenda_Artigos_ArtigosId",
                        column: x => x.ArtigosId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigoEncomenda_Encomendas_EncomendasId",
                        column: x => x.EncomendasId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Armazem_ResponsavelFK",
                table: "Armazem",
                column: "ResponsavelFK");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigoEncomenda_EncomendasId",
                table: "ArtigoEncomenda",
                column: "EncomendasId");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_ArmazemFK",
                table: "Artigos",
                column: "ArmazemFK");


            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_ClienteFK",
                table: "Encomendas",
                column: "ClienteFK");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_ChefeFK",
                table: "Funcionarios",
                column: "ChefeFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtigoEncomenda");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "Encomendas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Armazem");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Gestor");
        }
    }
}
