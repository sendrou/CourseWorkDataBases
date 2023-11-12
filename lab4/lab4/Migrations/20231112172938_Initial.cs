using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab4.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrands",
                columns: table => new
                {
                    CarBrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarBrand__3EAE0B29712E2DC6", x => x.CarBrandID);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    PassportDetails = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Drivers__F1B1CD24A6D77012", x => x.DriverID);
                });

            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    LoadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Loads__4ED77A2DE7E6350B", x => x.LoadID);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Organiza__CADB0B72E36729C0", x => x.OrganizationID);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    SettlementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettlementName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Settleme__771254BA6604459B", x => x.SettlementID);
                });

            migrationBuilder.CreateTable(
                name: "TransportationTariffs",
                columns: table => new
                {
                    TransportationTariffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TariffPertkm = table.Column<int>(name: "TariffPer(t*km)", type: "int", nullable: false),
                    TariffPerm3km = table.Column<int>(name: "TariffPer(m^3*km)", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transpor__A3368695B734D0C0", x => x.TransportationTariffID);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBrandID = table.Column<int>(type: "int", nullable: true),
                    LiftingCapacity = table.Column<int>(type: "int", nullable: false),
                    BodyVolume = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cars__68A0340E67C70070", x => x.CarID);
                    table.ForeignKey(
                        name: "FK__Cars__CarBrandID__4222D4EF",
                        column: x => x.CarBrandID,
                        principalTable: "CarBrands",
                        principalColumn: "CarBrandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distances",
                columns: table => new
                {
                    DistanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeparturesSettlementID = table.Column<int>(type: "int", nullable: true),
                    ArrivalSettlementID = table.Column<int>(type: "int", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Distance__A24E2A1C2181A126", x => x.DistanceID);
                    table.ForeignKey(
                        name: "FK__Distances__Arriv__3B75D760",
                        column: x => x.ArrivalSettlementID,
                        principalTable: "Settlements",
                        principalColumn: "SettlementID");
                    table.ForeignKey(
                        name: "FK__Distances__Depar__3A81B327",
                        column: x => x.DeparturesSettlementID,
                        principalTable: "Settlements",
                        principalColumn: "SettlementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoTransportation",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    OrganizationID = table.Column<int>(type: "int", nullable: true),
                    DistanceID = table.Column<int>(type: "int", nullable: true),
                    DriverID = table.Column<int>(type: "int", nullable: true),
                    CarID = table.Column<int>(type: "int", nullable: true),
                    LoadID = table.Column<int>(type: "int", nullable: true),
                    TransportationTariffID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CargoTra__1ABEEF6F97EC5565", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK__CargoTran__CarID__4BAC3F29",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CargoTran__Dista__49C3F6B7",
                        column: x => x.DistanceID,
                        principalTable: "Distances",
                        principalColumn: "DistanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CargoTran__Drive__4AB81AF0",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CargoTran__LoadI__4CA06362",
                        column: x => x.LoadID,
                        principalTable: "Loads",
                        principalColumn: "LoadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CargoTran__Organ__48CFD27E",
                        column: x => x.OrganizationID,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CargoTran__Trans__4D94879B",
                        column: x => x.TransportationTariffID,
                        principalTable: "TransportationTariffs",
                        principalColumn: "TransportationTariffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransportation_CarID",
                table: "CargoTransportation",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransportation_DistanceID",
                table: "CargoTransportation",
                column: "DistanceID");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransportation_DriverID",
                table: "CargoTransportation",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransportation_LoadID",
                table: "CargoTransportation",
                column: "LoadID");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransportation_OrganizationID",
                table: "CargoTransportation",
                column: "OrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransportation_TransportationTariffID",
                table: "CargoTransportation",
                column: "TransportationTariffID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarBrandID",
                table: "Cars",
                column: "CarBrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Distances_ArrivalSettlementID",
                table: "Distances",
                column: "ArrivalSettlementID");

            migrationBuilder.CreateIndex(
                name: "IX_Distances_DeparturesSettlementID",
                table: "Distances",
                column: "DeparturesSettlementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoTransportation");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Distances");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "TransportationTariffs");

            migrationBuilder.DropTable(
                name: "CarBrands");

            migrationBuilder.DropTable(
                name: "Settlements");
        }
    }
}
