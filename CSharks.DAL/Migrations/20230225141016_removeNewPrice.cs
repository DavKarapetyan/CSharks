using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharks.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeNewPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Comics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "NewPrice",
                table: "Comics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
