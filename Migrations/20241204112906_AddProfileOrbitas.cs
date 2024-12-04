using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcStarter.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileOrbitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileGroupsDTO",
                table: "ProfileGroupsDTO");

            migrationBuilder.RenameTable(
                name: "ProfileGroupsDTO",
                newName: "ProfileGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileGroups",
                table: "ProfileGroups",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProfileOrbitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileGroupID = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileOrbitas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileOrbitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileGroups",
                table: "ProfileGroups");

            migrationBuilder.RenameTable(
                name: "ProfileGroups",
                newName: "ProfileGroupsDTO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileGroupsDTO",
                table: "ProfileGroupsDTO",
                column: "Id");
        }
    }
}
