using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharks.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixcomics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageFile",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Comics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NewPrice",
                table: "Comics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Comics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Comics");

            migrationBuilder.AlterColumn<string>(
                name: "ImageFile",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
