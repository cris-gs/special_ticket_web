using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1SpecialTicket.Migrations
{
    /// <inheritdoc />
    public partial class IdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "escenario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_spanish_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    localizacion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_spanish_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<string>(type: "longtext", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated_By = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
               name: "Users",
               columns: table => new
               {
                   Id = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   UserName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   NormalizedUserName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   Email = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   NormalizedEmail = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                   Discriminator = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   PasswordHash = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   SecurityStamp = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   PhoneNumber = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                       .Annotation("MySql:CharSet", "utf8mb4"),
                   PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                   TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                   LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                   LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                   AccessFailedCount = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Users", x => x.Id);
               })
               .Annotation("MySql:CharSet", "utf8mb4")
               .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "RoleClaims_ibfk_1",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "tipo_evento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_spanish_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<string>(type: "longtext", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated_By = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "UserClaims_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "UserLogins_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "UserRoles_ibfk_1",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "UserRoles_ibfk_2",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "UserTokens_ibfk_1",
                        column: x => x.UserId, 
                        principalTable: "Users", 
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<int>(type: "int", nullable: false),
                    rol = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<int>(type: "int", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated_By = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "asiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_spanish_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<string>(type: "longtext", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated_By = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_escenario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "asiento_ibfk_1",
                        column: x => x.id_escenario,
                        principalTable: "escenario",
                        principalColumn: "id");
                },
                comment: "tipos de asiento del escenario")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "tipo_escenario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_spanish_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<string>(type: "longtext", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated_By = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_escenario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "tipo_escenario_ibfk_1",
                        column: x => x.id_escenario,
                        principalTable: "escenario",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "evento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_spanish_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<string>(type: "longtext", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated_By = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_tipo_evento = table.Column<int>(type: "int", nullable: false),
                    id_escenario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "evento_ibfk_1",
                        column: x => x.id_escenario,
                        principalTable: "escenario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "evento_ibfk_2",
                        column: x => x.id_tipo_evento,
                        principalTable: "tipo_evento",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "entradas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Disponibles = table.Column<int>(type: "int", nullable: false),
                    tipo_asiento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_spanish_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    precio = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<string>(type: "longtext", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated_By = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_evento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "entradas_ibfk_1",
                        column: x => x.id_evento,
                        principalTable: "evento",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "compra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    fecha_reserva = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_pago = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Created_By = table.Column<string>(type: "longtext", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_cliente = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_entrada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "compra_ibfk_1",
                        column: x => x.id_cliente,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "compra_ibfk_2",
                        column: x => x.id_entrada,
                        principalTable: "entradas",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_spanish_ci");

            migrationBuilder.CreateIndex(
                name: "id_escenario",
                table: "asiento",
                column: "id_escenario");

            migrationBuilder.CreateIndex(
                name: "id_cliente",
                table: "compra",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "id_entrada",
                table: "compra",
                column: "id_entrada");

            migrationBuilder.CreateIndex(
                name: "id_evento",
                table: "entradas",
                column: "id_evento");

            migrationBuilder.CreateIndex(
                name: "id_escenario1",
                table: "evento",
                column: "id_escenario");

            migrationBuilder.CreateIndex(
                name: "id_tipo_evento",
                table: "evento",
                column: "id_tipo_evento");

            migrationBuilder.CreateIndex(
                name: "id_escenario2",
                table: "tipo_escenario",
                column: "id_escenario");

            migrationBuilder.CreateIndex(
                name: "UserId",
                table: "userroles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UserId",
                table: "usertokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UserId",
                table: "userlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
               name: "UserId",
               table: "userclaims",
               column: "UserId");

            migrationBuilder.CreateIndex(
               name: "RoleId",
               table: "roleclaims",
               column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asiento");

            migrationBuilder.DropTable(
                name: "compra");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "tipo_escenario");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "entradas");

            migrationBuilder.DropTable(
                name: "evento");

            migrationBuilder.DropTable(
                name: "escenario");

            migrationBuilder.DropTable(
                name: "tipo_evento");
        }
    }
}
