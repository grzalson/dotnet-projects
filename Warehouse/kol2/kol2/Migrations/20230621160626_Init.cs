using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehousesAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObjectTypes",
                columns: table => new
                {
                    IdObjectType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectTypes", x => x.IdObjectType);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.IdOwner);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    IdWarehouse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.IdWarehouse);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    IdObject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWarehouse = table.Column<int>(type: "int", nullable: false),
                    IdObjectType = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.IdObject);
                    table.ForeignKey(
                        name: "FK_Objects_ObjectTypes_IdObjectType",
                        column: x => x.IdObjectType,
                        principalTable: "ObjectTypes",
                        principalColumn: "IdObjectType");
                    table.ForeignKey(
                        name: "FK_Objects_Warehouses_IdWarehouse",
                        column: x => x.IdWarehouse,
                        principalTable: "Warehouses",
                        principalColumn: "IdWarehouse");
                });

            migrationBuilder.CreateTable(
                name: "ObjectOwners",
                columns: table => new
                {
                    IdObject = table.Column<int>(type: "int", nullable: false),
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectOwners", x => new { x.IdObject, x.IdOwner });
                    table.ForeignKey(
                        name: "FK_ObjectOwners_Objects_IdObject",
                        column: x => x.IdObject,
                        principalTable: "Objects",
                        principalColumn: "IdObject");
                    table.ForeignKey(
                        name: "FK_ObjectOwners_Owners_IdOwner",
                        column: x => x.IdOwner,
                        principalTable: "Owners",
                        principalColumn: "IdOwner");
                });

            migrationBuilder.InsertData(
                table: "ObjectTypes",
                columns: new[] { "IdObjectType", "Name" },
                values: new object[,]
                {
                    { 1, "Obiekt A" },
                    { 2, "Obiekt B" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "IdOwner", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Jan", "Kowalski", "123456789" },
                    { 2, "Anna", "Nowak", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "IdWarehouse", "Name" },
                values: new object[,]
                {
                    { 1, "Magazyn A" },
                    { 2, "Magazyn B" }
                });

            migrationBuilder.InsertData(
                table: "Objects",
                columns: new[] { "IdObject", "Height", "IdObjectType", "IdWarehouse", "Width" },
                values: new object[,]
                {
                    { 1, 10.0, 1, 1, 20.0 },
                    { 2, 15.0, 2, 1, 25.0 }
                });

            migrationBuilder.InsertData(
                table: "ObjectOwners",
                columns: new[] { "IdObject", "IdOwner" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjectOwners_IdOwner",
                table: "ObjectOwners",
                column: "IdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_IdObjectType",
                table: "Objects",
                column: "IdObjectType");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_IdWarehouse",
                table: "Objects",
                column: "IdWarehouse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectOwners");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "ObjectTypes");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
