using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Chuvashia.NationalProjects.Migrations
{
    public partial class news : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 258, nullable: false),
                    Text = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPosts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPosts");
        }
    }
}
