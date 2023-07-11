using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaLegos.Migrations
{
    public partial class roleseedAndLorem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    País = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrTelemovel = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    NrContribuinte = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Local = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    Nr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrPecas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalhes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Stock = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "ArtigoEncomendas",
                columns: table => new
                {
                    ArtigoId = table.Column<int>(type: "int", nullable: false),
                    EncomendaId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigoEncomendas", x => new { x.ArtigoId, x.EncomendaId });
                    table.ForeignKey(
                        name: "FK_ArtigoEncomendas_Artigos_ArtigoId",
                        column: x => x.ArtigoId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigoEncomendas_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c", "78368bff-e7fe-4e7f-81af-851a7ac2a750", "Cliente", "CLIENTE" },
                    { "f", "e9ddb7dc-83ef-4c70-aecc-f058d3bd5e81", "Funcionario", "FUNCIONARIO" },
                    { "g", "49b60ac9-795a-4704-97a6-369562b9c824", "Gestor", "GESTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "c9f12f51-6937-4664-8fb8-55cb77cec00c", "ApplicationUser", "cliente2@gmail.com", true, true, null, "CLIENTE2@GMAIL.COM", "CLIENTE2@GMAIL.COM", "AQAAAAEAACcQAAAAEB8ri/Jh9mJOfgyIYn44D2tLdAg8PpADgmkyapOOCfo6H6uRgx5Zc6S5Gc87Z4C0yg==", null, false, "faebdb29-0f35-40cd-861d-d74f8026b474", false, "cliente2@gmail.com" },
                    { "2", 0, "61bf67bc-3409-4a64-9bc2-199f48ee7ea9", "ApplicationUser", "beatrizpatita@blabla.com", true, true, null, "BEATRIZPATITA@BLABLA.COM", "BEATRIZPATITA@BLABLA.COM", "AQAAAAEAACcQAAAAEPM4KGDyIOFfCJ/JKmZQl7YmbJotfYDeoOVoH5T0VFVDzKKCPL9H+6nfFo8xHbejeQ==", null, false, "4584002c-38ec-4cef-8bd1-81bfd720ee25", false, "beatrizpatita@blabla.com" },
                    { "3", 0, "5c616fb9-cf87-4bd6-b15c-17fd86747add", "ApplicationUser", "Luisfreitas@blabla.com", true, true, null, "LUISFREITAS@BLABLA.COM", "LUISFREITAS@BLABLA.COM", "AQAAAAEAACcQAAAAEBRJjKhPesIXxPsEKpjMobgVFJQ4rf8Leo4UEzb5mmWxmwDx7/qzLhYeDx2jdrG2bw==", null, false, "2304439b-af6f-4eb3-8ad2-a1fed07be74d", false, "Luisfreitas@blabla.com" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Apelido", "Cidade", "CodPostal", "Email", "Morada", "NrContribuinte", "NrTelemovel", "País", "PrimeiroNome", "UserId" },
                values: new object[] { 1, "Mendes", "Santo António dos Cavaleiros", "2660-284", "goncalomendes@sapo.pt", "Rua...", "412414141", "941941941", "Portugal", "Gonçalo", "1" });

            migrationBuilder.InsertData(
                table: "Gestor",
                columns: new[] { "Id", "Cargo", "Email", "Foto", "Nome", "NrTelemovel", "UserId" },
                values: new object[] { 1, "Gestor", "luisfreitas@blabla.com", "", "Luís Freitas", "943943943", "3" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c", "1" },
                    { "f", "2" },
                    { "g", "3" }
                });

            migrationBuilder.InsertData(
                table: "Encomendas",
                columns: new[] { "Id", "ClienteFK", "Data", "Estado", "Total" },
                values: new object[] { 1, 1, new DateTime(2023, 6, 25, 13, 58, 56, 0, DateTimeKind.Unspecified), "pago", "3000" });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "Cargo", "ChefeFK", "Email", "Nome", "NrTelemovel", "UserId" },
                values: new object[] { 1, "Funcionário", 1, "beatrizpatita@blabla.com", "Beatriz", "942942942", "2" });

            migrationBuilder.InsertData(
                table: "Armazem",
                columns: new[] { "Id", "Local", "ResponsavelFK" },
                values: new object[] { 1, "Tomar", 1 });

            migrationBuilder.InsertData(
                table: "Artigos",
                columns: new[] { "Id", "ArmazemFK", "Detalhes", "Foto", "Nome", "Nr", "NrPecas", "Preco", "Stock", "Tipo" },
                values: new object[,]
                {
                    { 1, 1, "", "42004.jpg", "Mini blackhoe Loader", "42004", "97", 113.99m, "5", "Technic" },
                    { 2, 1, "l", "42057.jpg", "Ultralight Helicopter", "42057", "105", 119.99m, "99", "Technic" },
                    { 3, 1, "", "60239.jpg", "Police Car", "60238", "94", 19.99m, "36", "City" },
                    { 4, 1, "", "60292.jpg", "Headquarters", "60292", "790", 64.99m, "47", "City" },
                    { 5, 1, "", "6157.jpg", "Zoo", "6157", "101", 49.99m, "93", "Duplo" },
                    { 6, 1, "", "10959.jpg", "Police Station", "10959", "40", 49.99m, "12", "Duplo" },
                    { 7, 1, "", "8831.jpg", "Atlas", "8831", "1", 9.99m, "68", "Minifigures" },
                    { 8, 1, "", "71011.jpg", "Field Worker", "71011", "1", 9.99m, "76", "Minifigures" }
                });

            migrationBuilder.InsertData(
                table: "ArtigoEncomendas",
                columns: new[] { "ArtigoId", "EncomendaId", "Quantidade" },
                values: new object[] { 1, 1, 23 });

            migrationBuilder.InsertData(
                table: "ArtigoEncomendas",
                columns: new[] { "ArtigoId", "EncomendaId", "Quantidade" },
                values: new object[] { 2, 1, 26 });

            migrationBuilder.CreateIndex(
                name: "IX_Armazem_ResponsavelFK",
                table: "Armazem",
                column: "ResponsavelFK");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigoEncomendas_EncomendaId",
                table: "ArtigoEncomendas",
                column: "EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_ArmazemFK",
                table: "Artigos",
                column: "ArmazemFK");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "ArtigoEncomendas");

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
