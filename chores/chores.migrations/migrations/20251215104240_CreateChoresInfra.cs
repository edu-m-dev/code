using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chores.migrations.migrations
{
    /// <inheritdoc />
    public partial class CreateChoresInfra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chores_Id",
                table: "chores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "completed_chores");

            migrationBuilder.DropTable(
                name: "chores");
        }
    }
}
