using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SupportAPI.Services.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    IdProfile = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Formation = table.Column<string>(maxLength: 500, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 500, nullable: true),
                    Professionals = table.Column<int>(nullable: false),
                    Employes = table.Column<int>(nullable: false),
                    Department = table.Column<string>(maxLength: 500, nullable: true),
                    Province = table.Column<string>(maxLength: 500, nullable: true),
                    Municipality = table.Column<string>(maxLength: 500, nullable: true),
                    WaterConnections = table.Column<int>(nullable: false),
                    ConnectionsWithMeter = table.Column<int>(nullable: false),
                    ConnectionsWithoutMeter = table.Column<int>(nullable: false),
                    PublicPools = table.Column<int>(nullable: false),
                    Latrines = table.Column<int>(nullable: false),
                    ServiceContinuity = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.IdProfile);
                });

            migrationBuilder.CreateTable(
                name: "UserKobo",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKobo", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    IdOrganization = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProfile = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.IdOrganization);
                    table.ForeignKey(
                        name: "FK_Organization_Profile_IdProfile",
                        column: x => x.IdProfile,
                        principalTable: "Profile",
                        principalColumn: "IdProfile",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportApiUser",
                columns: table => new
                {
                    Username = table.Column<string>(maxLength: 500, nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: true),
                    IdOrganization = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportApiUser", x => x.Username);
                    table.ForeignKey(
                        name: "FK_SupportApiUser_Organization_IdOrganization",
                        column: x => x.IdOrganization,
                        principalTable: "Organization",
                        principalColumn: "IdOrganization",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportApiUser_UserKobo",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportApiUser_UserKobo", x => new { x.Name, x.Username });
                    table.ForeignKey(
                        name: "FK_SupportApiUser_UserKobo_UserKobo_Name",
                        column: x => x.Name,
                        principalTable: "UserKobo",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportApiUser_UserKobo_SupportApiUser_Username",
                        column: x => x.Username,
                        principalTable: "SupportApiUser",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organization_IdProfile",
                table: "Organization",
                column: "IdProfile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportApiUser_IdOrganization",
                table: "SupportApiUser",
                column: "IdOrganization");

            migrationBuilder.CreateIndex(
                name: "IX_SupportApiUser_UserKobo_Username",
                table: "SupportApiUser_UserKobo",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportApiUser_UserKobo");

            migrationBuilder.DropTable(
                name: "UserKobo");

            migrationBuilder.DropTable(
                name: "SupportApiUser");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Profile");
        }
    }
}
