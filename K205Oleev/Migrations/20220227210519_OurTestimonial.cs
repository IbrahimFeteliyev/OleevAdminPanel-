using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K205Oleev.Migrations
{
    public partial class OurTestimonial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OurTestimonials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurTestimonials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OurTestimonialLanguages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LangCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SEO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OurTestimonialID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurTestimonialLanguages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OurTestimonialLanguages_OurTestimonials_OurTestimonialID",
                        column: x => x.OurTestimonialID,
                        principalTable: "OurTestimonials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OurTestimonialLanguages_OurTestimonialID",
                table: "OurTestimonialLanguages",
                column: "OurTestimonialID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurTestimonialLanguages");

            migrationBuilder.DropTable(
                name: "OurTestimonials");
        }
    }
}
