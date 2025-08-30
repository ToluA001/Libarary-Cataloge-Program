using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libarary_Cataloge_Program.Migrations
{
    /// <inheritdoc />
    public partial class LibID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryID",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LibraryID",
                table: "Books");
        }
    }
}
