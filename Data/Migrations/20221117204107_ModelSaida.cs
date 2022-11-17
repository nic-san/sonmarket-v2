using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sonmarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelSaida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Quantidade",
                table: "Saidas",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Saidas");
        }
    }
}
