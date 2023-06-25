using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MechanicKol.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    IdMake = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.IdMake);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    IdSpecialization = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.IdSpecialization);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    IdCar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationPlate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductionYear = table.Column<DateTime>(type: "date", nullable: false),
                    IdMake = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.IdCar);
                    table.ForeignKey(
                        name: "FK_Cars_Makes_IdMake",
                        column: x => x.IdMake,
                        principalTable: "Makes",
                        principalColumn: "IdMake");
                });

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    IdMechanic = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdSpecialization = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.IdMechanic);
                    table.ForeignKey(
                        name: "FK_Mechanics_Specializations_IdSpecialization",
                        column: x => x.IdSpecialization,
                        principalTable: "Specializations",
                        principalColumn: "IdSpecialization");
                });

            migrationBuilder.CreateTable(
                name: "MechanicCars",
                columns: table => new
                {
                    IdMechanic = table.Column<int>(type: "int", nullable: false),
                    IdCar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicCars", x => new { x.IdMechanic, x.IdCar });
                    table.ForeignKey(
                        name: "FK_MechanicCars_Cars_IdCar",
                        column: x => x.IdCar,
                        principalTable: "Cars",
                        principalColumn: "IdCar");
                    table.ForeignKey(
                        name: "FK_MechanicCars_Mechanics_IdMechanic",
                        column: x => x.IdMechanic,
                        principalTable: "Mechanics",
                        principalColumn: "IdMechanic");
                });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "IdMake", "Name" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Honda" },
                    { 3, "BMW" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "IdSpecialization", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Engine" },
                    { 3, "Bodywork" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "IdCar", "IdMake", "ProductionYear", "RegistrationPlate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ABC123" },
                    { 2, 2, new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "XYZ789" },
                    { 3, 3, new DateTime(2018, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "DEF456" }
                });

            migrationBuilder.InsertData(
                table: "Mechanics",
                columns: new[] { "IdMechanic", "FirstName", "IdSpecialization", "LastName", "Nickname" },
                values: new object[,]
                {
                    { 1, "John", 1, "Doe", "JD" },
                    { 2, "Jane", 2, "Smith", "JS" },
                    { 3, "Robert", 3, "Johnson", null }
                });

            migrationBuilder.InsertData(
                table: "MechanicCars",
                columns: new[] { "IdCar", "IdMechanic" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_IdMake",
                table: "Cars",
                column: "IdMake");

            migrationBuilder.CreateIndex(
                name: "IX_MechanicCars_IdCar",
                table: "MechanicCars",
                column: "IdCar");

            migrationBuilder.CreateIndex(
                name: "IX_Mechanics_IdSpecialization",
                table: "Mechanics",
                column: "IdSpecialization");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MechanicCars");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Mechanics");

            migrationBuilder.DropTable(
                name: "Makes");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
