using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libarary_Cataloge_Program.Migrations.AuthDbMigrations
{
    /// <inheritdoc />
    public partial class RemoveEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
