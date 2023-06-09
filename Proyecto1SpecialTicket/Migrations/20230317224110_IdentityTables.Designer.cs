﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proyecto1SpecialTicket.Models;

#nullable disable

namespace Proyecto1SpecialTicket.Migrations
{
    [DbContext(typeof(SpecialticketContext))]
    [Migration("20230317224110_IdentityTables")]
    partial class IdentityTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Asiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcion")
                        .UseCollation("utf8mb3_spanish_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Descripcion"), "utf8mb3");

                    b.Property<int>("IdEscenario")
                        .HasColumnType("int")
                        .HasColumnName("id_escenario");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdEscenario" }, "id_escenario");

                    b.ToTable("asiento", null, t =>
                        {
                            t.HasComment("tipos de asiento del escenario");
                        });
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_pago");

                    b.Property<DateTime>("FechaReserva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_reserva")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("id_cliente");

                    b.Property<int>("IdEntrada")
                        .HasColumnType("int")
                        .HasColumnName("id_entrada");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCliente" }, "id_cliente");

                    b.HasIndex(new[] { "IdEntrada" }, "id_entrada");

                    b.ToTable("compra", (string)null);

                    MySqlEntityTypeBuilderExtensions.HasCharSet(b, "utf8mb3");
                    MySqlEntityTypeBuilderExtensions.UseCollation(b, "utf8mb3_spanish_ci");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Entrada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<int>("Disponibles")
                        .HasColumnType("int");

                    b.Property<int>("IdEvento")
                        .HasColumnType("int")
                        .HasColumnName("id_evento");

                    b.Property<decimal>("Precio")
                        .HasPrecision(10)
                        .HasColumnType("decimal(10)")
                        .HasColumnName("precio");

                    b.Property<string>("TipoAsiento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("tipo_asiento")
                        .UseCollation("utf8mb3_spanish_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("TipoAsiento"), "utf8mb3");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdEvento" }, "id_evento");

                    b.ToTable("entradas", (string)null);
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Escenario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<string>("Localizacion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("localizacion")
                        .UseCollation("utf8mb3_spanish_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Localizacion"), "utf8mb3");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre")
                        .UseCollation("utf8mb3_spanish_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Nombre"), "utf8mb3");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("escenario", (string)null);
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcion")
                        .UseCollation("utf8mb3_spanish_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Descripcion"), "utf8mb3");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha");

                    b.Property<int>("IdEscenario")
                        .HasColumnType("int")
                        .HasColumnName("id_escenario");

                    b.Property<int>("IdTipoEvento")
                        .HasColumnType("int")
                        .HasColumnName("id_tipo_evento");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdEscenario" }, "id_escenario")
                        .HasDatabaseName("id_escenario1");

                    b.HasIndex(new[] { "IdTipoEvento" }, "id_tipo_evento");

                    b.ToTable("evento", (string)null);
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.TipoEscenario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcion")
                        .UseCollation("utf8mb3_spanish_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Descripcion"), "utf8mb3");

                    b.Property<int>("IdEscenario")
                        .HasColumnType("int")
                        .HasColumnName("id_escenario");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdEscenario" }, "id_escenario")
                        .HasDatabaseName("id_escenario2");

                    b.ToTable("tipo_escenario", (string)null);
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.TipoEvento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcion")
                        .UseCollation("utf8mb3_spanish_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Descripcion"), "utf8mb3");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("tipo_evento", (string)null);
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("correo");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Created_By");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.Property<int>("Rol")
                        .HasColumnType("int")
                        .HasColumnName("rol");

                    b.Property<int>("Telefono")
                        .HasColumnType("int")
                        .HasColumnName("telefono");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("Updated_By");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Asiento", b =>
                {
                    b.HasOne("Proyecto1SpecialTicket.Models.Escenario", "IdEscenarioNavigation")
                        .WithMany("Asientos")
                        .HasForeignKey("IdEscenario")
                        .IsRequired()
                        .HasConstraintName("asiento_ibfk_1");

                    b.Navigation("IdEscenarioNavigation");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Compra", b =>
                {
                    b.HasOne("Proyecto1SpecialTicket.Models.Usuario", "IdClienteNavigation")
                        .WithMany("Compras")
                        .HasForeignKey("IdCliente")
                        .IsRequired()
                        .HasConstraintName("compra_ibfk_1");

                    b.HasOne("Proyecto1SpecialTicket.Models.Entrada", "IdEntradaNavigation")
                        .WithMany("Compras")
                        .HasForeignKey("IdEntrada")
                        .IsRequired()
                        .HasConstraintName("compra_ibfk_2");

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdEntradaNavigation");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Entrada", b =>
                {
                    b.HasOne("Proyecto1SpecialTicket.Models.Evento", "IdEventoNavigation")
                        .WithMany("Entrada")
                        .HasForeignKey("IdEvento")
                        .IsRequired()
                        .HasConstraintName("entradas_ibfk_1");

                    b.Navigation("IdEventoNavigation");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Evento", b =>
                {
                    b.HasOne("Proyecto1SpecialTicket.Models.Escenario", "IdEscenarioNavigation")
                        .WithMany("Eventos")
                        .HasForeignKey("IdEscenario")
                        .IsRequired()
                        .HasConstraintName("evento_ibfk_1");

                    b.HasOne("Proyecto1SpecialTicket.Models.TipoEvento", "IdTipoEventoNavigation")
                        .WithMany("Eventos")
                        .HasForeignKey("IdTipoEvento")
                        .IsRequired()
                        .HasConstraintName("evento_ibfk_2");

                    b.Navigation("IdEscenarioNavigation");

                    b.Navigation("IdTipoEventoNavigation");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.TipoEscenario", b =>
                {
                    b.HasOne("Proyecto1SpecialTicket.Models.Escenario", "IdEscenarioNavigation")
                        .WithMany("TipoEscenarios")
                        .HasForeignKey("IdEscenario")
                        .IsRequired()
                        .HasConstraintName("tipo_escenario_ibfk_1");

                    b.Navigation("IdEscenarioNavigation");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Entrada", b =>
                {
                    b.Navigation("Compras");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Escenario", b =>
                {
                    b.Navigation("Asientos");

                    b.Navigation("Eventos");

                    b.Navigation("TipoEscenarios");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Evento", b =>
                {
                    b.Navigation("Entrada");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.TipoEvento", b =>
                {
                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("Proyecto1SpecialTicket.Models.Usuario", b =>
                {
                    b.Navigation("Compras");
                });
#pragma warning restore 612, 618
        }
    }
}
