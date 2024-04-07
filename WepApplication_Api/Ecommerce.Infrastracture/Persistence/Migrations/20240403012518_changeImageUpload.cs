
#nullable disable

namespace Ecommerce.Infrastracture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeImageUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Products");
        }
    }
}
