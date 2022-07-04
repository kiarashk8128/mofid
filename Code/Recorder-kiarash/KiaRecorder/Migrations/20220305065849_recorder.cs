using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiaRecorder.Migrations
{
    public partial class recorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reecorders",
                columns: table => new
                {
                    EntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timer = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Random = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reecorders", x => x.EntryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reecorders");
        }
    }
}
