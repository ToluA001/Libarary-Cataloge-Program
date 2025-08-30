using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libarary_Cataloge_Program.Migrations.AuthDbMigrations
{
    /// <inheritdoc />
    public partial class isloged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoggedIn",
                table: "users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLoggedIn",
                table: "users");
        }
    }
}
