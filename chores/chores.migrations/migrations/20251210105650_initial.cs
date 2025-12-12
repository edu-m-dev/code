using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chores.migrations.migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "completed_chores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChoreId = table.Column<long>(type: "INTEGER", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    Date = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_completed_chores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_completed_chores_chores_ChoreId",
                        column: x => x.ChoreId,
                        principalTable: "chores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chores_Id",
                table: "chores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_completed_chores_ChoreId",
                table: "completed_chores",
                column: "ChoreId");

            migrationBuilder.CreateIndex(
                name: "IX_completed_chores_Id",
                table: "completed_chores",
                column: "Id",
                unique: true);
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
