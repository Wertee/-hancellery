using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProjectMVC.Migrations.GameStoreIdentity
{
    public partial class UserMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserLastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserLastName",
                table: "AspNetUsers");
        }
    }
}
