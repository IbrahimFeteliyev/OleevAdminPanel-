using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K205Oleev.Migrations
{
    public partial class OurExpertise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OurExpertises",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurExpertises", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OurExpertiseLanguages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LangCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SEO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OurExpertiseID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurExpertiseLanguages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OurExpertiseLanguages_OurExpertises_OurExpertiseID",
                        column: x => x.OurExpertiseID,
                        principalTable: "OurExpertises",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OurExpertiseLanguages_OurExpertiseID",
                table: "OurExpertiseLanguages",
                column: "OurExpertiseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurExpertiseLanguages");

            migrationBuilder.DropTable(
                name: "OurExpertises");
        }
    }
}
