using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoRepairShop.Web.Migrations
{
    public partial class SetDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    BrandName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ColorName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    FuelType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ModelName = table.Column<string>(maxLength: 20, nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LicencePlate = table.Column<string>(maxLength: 10, nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    EngineCapacity = table.Column<string>(nullable: false),
                    FuelId = table.Column<int>(nullable: false),
                    ColorId = table.Column<string>(nullable: true),
                    ColorId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Colors_ColorId1",
                        column: x => x.ColorId1,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandName",
                table: "Brands",
                column: "BrandName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorName",
                table: "Colors",
                column: "ColorName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_FuelType",
                table: "Fuels",
                column: "FuelType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_ModelName",
                table: "Models",
                column: "ModelName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ColorId1",
                table: "Vehicles",
                column: "ColorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FuelId",
                table: "Vehicles",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicencePlate",
                table: "Vehicles",
                column: "LicencePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelId",
                table: "Vehicles",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
