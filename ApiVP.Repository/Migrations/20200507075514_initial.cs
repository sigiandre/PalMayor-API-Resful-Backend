using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiVP.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    DNI = table.Column<string>(maxLength: 8, nullable: false),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RangoHoras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inicio = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    Fin = table.Column<TimeSpan>(type: "time(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangoHoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Contrasenya = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ABVCs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    TipoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABVCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ABVCs_Tipos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Tipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enfermeros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colegiatura = table.Column<string>(type: "varchar(8)", nullable: false),
                    Universidad = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Experiencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(nullable: false),
                    PersonaId = table.Column<int>(nullable: false),
                    EspecialidadId = table.Column<int>(nullable: false),
                    GradoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enfermeros_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enfermeros_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enfermeros_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enfermeros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Familiares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(nullable: false),
                    PersonaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familiares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Familiares_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Familiares_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ancianos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamiliarId = table.Column<int>(nullable: false),
                    PersonaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ancianos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ancianos_Familiares_FamiliarId",
                        column: x => x.FamiliarId,
                        principalTable: "Familiares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ancianos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AncianoABVCs",
                columns: table => new
                {
                    AncianoId = table.Column<int>(nullable: false),
                    ABVCId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AncianoABVCs", x => new { x.AncianoId, x.ABVCId });
                    table.ForeignKey(
                        name: "FK_AncianoABVCs_ABVCs_ABVCId",
                        column: x => x.ABVCId,
                        principalTable: "ABVCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AncianoABVCs_Ancianos_AncianoId",
                        column: x => x.AncianoId,
                        principalTable: "Ancianos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "varchar(10)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AncianoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertas_Ancianos_AncianoId",
                        column: x => x.AncianoId,
                        principalTable: "Ancianos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnfermeroOfertas",
                columns: table => new
                {
                    EnfermeroId = table.Column<int>(nullable: false),
                    OfertaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnfermeroOfertas", x => new { x.EnfermeroId, x.OfertaId });
                    table.ForeignKey(
                        name: "FK_EnfermeroOfertas_Enfermeros_EnfermeroId",
                        column: x => x.EnfermeroId,
                        principalTable: "Enfermeros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnfermeroOfertas_Ofertas_OfertaId",
                        column: x => x.OfertaId,
                        principalTable: "Ofertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FechaAtenciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    RangoHoraId = table.Column<int>(nullable: false),
                    OfertaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechaAtenciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FechaAtenciones_Ofertas_OfertaId",
                        column: x => x.OfertaId,
                        principalTable: "Ofertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FechaAtenciones_RangoHoras_RangoHoraId",
                        column: x => x.RangoHoraId,
                        principalTable: "RangoHoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "varchar(20)", nullable: true),
                    Costo = table.Column<decimal>(type: "money", nullable: false),
                    EnfermeroId = table.Column<int>(nullable: false),
                    OfertaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicios_Enfermeros_EnfermeroId",
                        column: x => x.EnfermeroId,
                        principalTable: "Enfermeros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicios_Ofertas_OfertaId",
                        column: x => x.OfertaId,
                        principalTable: "Ofertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ABVCs_TipoId",
                table: "ABVCs",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_AncianoABVCs_ABVCId",
                table: "AncianoABVCs",
                column: "ABVCId");

            migrationBuilder.CreateIndex(
                name: "IX_Ancianos_FamiliarId",
                table: "Ancianos",
                column: "FamiliarId");

            migrationBuilder.CreateIndex(
                name: "IX_Ancianos_PersonaId",
                table: "Ancianos",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_EnfermeroOfertas_OfertaId",
                table: "EnfermeroOfertas",
                column: "OfertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeros_EspecialidadId",
                table: "Enfermeros",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeros_GradoId",
                table: "Enfermeros",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeros_PersonaId",
                table: "Enfermeros",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeros_UsuarioId",
                table: "Enfermeros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Familiares_PersonaId",
                table: "Familiares",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Familiares_UsuarioId",
                table: "Familiares",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FechaAtenciones_OfertaId",
                table: "FechaAtenciones",
                column: "OfertaId");

            migrationBuilder.CreateIndex(
                name: "IX_FechaAtenciones_RangoHoraId",
                table: "FechaAtenciones",
                column: "RangoHoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_AncianoId",
                table: "Ofertas",
                column: "AncianoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_EnfermeroId",
                table: "Servicios",
                column: "EnfermeroId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_OfertaId",
                table: "Servicios",
                column: "OfertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AncianoABVCs");

            migrationBuilder.DropTable(
                name: "EnfermeroOfertas");

            migrationBuilder.DropTable(
                name: "FechaAtenciones");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "ABVCs");

            migrationBuilder.DropTable(
                name: "RangoHoras");

            migrationBuilder.DropTable(
                name: "Enfermeros");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "Tipos");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "Grados");

            migrationBuilder.DropTable(
                name: "Ancianos");

            migrationBuilder.DropTable(
                name: "Familiares");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
