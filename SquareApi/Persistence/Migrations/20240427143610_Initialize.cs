using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquareApi.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Square",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Point1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Point2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Point3Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Point4Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Square", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Square_Point_Point1Id",
                        column: x => x.Point1Id,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Square_Point_Point2Id",
                        column: x => x.Point2Id,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Square_Point_Point3Id",
                        column: x => x.Point3Id,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Square_Point_Point4Id",
                        column: x => x.Point4Id,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Square_Point1Id",
                table: "Square",
                column: "Point1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Square_Point2Id",
                table: "Square",
                column: "Point2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Square_Point3Id",
                table: "Square",
                column: "Point3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Square_Point4Id",
                table: "Square",
                column: "Point4Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Square");

            migrationBuilder.DropTable(
                name: "Point");
        }
    }
}
