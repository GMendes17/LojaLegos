using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaLegos.Data.Migrations
{
    public partial class Inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c", "b1af593e-50d9-41e9-8321-fc775c592b3a", "Cliente", "CLIENTE" },
                    { "f", "594df188-0888-4af7-b49e-c7b666005678", "Funcionario", "FUNCIONARIO" },
                    { "g", "63338296-f987-4059-a576-15858dece603", "Gestor", "GESTOR" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Apelido", "Cidade", "CodPostal", "Email", "Morada", "NrContribuinte", "NrTelemovel", "País", "PrimeiroNome" },
                values: new object[] { 1, "Gomes", "Lisboa", "424242424242", "mariagomes@blabla.com", "Lisboa", "412414141", "941941941", "Portugal", "Maria" });

            migrationBuilder.InsertData(
                table: "Gestor",
                columns: new[] { "Id", "Cargo", "Email", "Foto", "Nome", "NrTelemovel" },
                values: new object[] { 1, "Gestor", "luisfreitas@blabla.com", "", "Luís Freitas", "943943943" });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "Cargo", "ChefeFK", "Email", "Nome", "NrTelemovel" },
                values: new object[] { 1, "Funcionário", 1, "beatrizpatita@blabla.com", "Beatriz", "942942942" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g");

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gestor",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
