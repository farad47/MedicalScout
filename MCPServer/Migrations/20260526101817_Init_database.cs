using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MCPServer.Migrations
{
    /// <inheritdoc />
    public partial class Init_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    PriceGross = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    PreparationInfo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodTestIndication",
                columns: table => new
                {
                    BloodTestsId = table.Column<int>(type: "integer", nullable: false),
                    IndicationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTestIndication", x => new { x.BloodTestsId, x.IndicationsId });
                    table.ForeignKey(
                        name: "FK_BloodTestIndication_BloodTests_BloodTestsId",
                        column: x => x.BloodTestsId,
                        principalTable: "BloodTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodTestIndication_Indications_IndicationsId",
                        column: x => x.IndicationsId,
                        principalTable: "Indications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodTestIndication_IndicationsId",
                table: "BloodTestIndication",
                column: "IndicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodTests_Code",
                table: "BloodTests",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Indications_Name",
                table: "Indications",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodTestIndication");

            migrationBuilder.DropTable(
                name: "BloodTests");

            migrationBuilder.DropTable(
                name: "Indications");
        }
    }
}
