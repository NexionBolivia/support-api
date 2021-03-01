using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Support.API.Services.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Path = table.Column<string>(maxLength: 500, nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProfiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(nullable: false)
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
                    ServiceContinuity = table.Column<string>(maxLength: 500, nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProfiles", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    IdProfile = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationProfiles_IdProfile",
                        column: x => x.IdProfile,
                        principalTable: "OrganizationProfiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesToKoboUsers",
                columns: table => new
                {
                    KoboUserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesToKoboUsers", x => new { x.KoboUserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolesToKoboUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleToAsset",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    AssetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleToAsset", x => new { x.RoleId, x.AssetId });
                    table.ForeignKey(
                        name: "FK_RoleToAsset_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleToAsset_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationsToKoboUsers",
                columns: table => new
                {
                    KoboUserId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsToKoboUsers", x => new { x.KoboUserId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_OrganizationsToKoboUsers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_IdProfile",
                table: "Organizations",
                column: "IdProfile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsToKoboUsers_OrganizationId",
                table: "OrganizationsToKoboUsers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesToKoboUsers_RoleId",
                table: "RolesToKoboUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleToAsset_AssetId",
                table: "RoleToAsset",
                column: "AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationsToKoboUsers");

            migrationBuilder.DropTable(
                name: "RolesToKoboUsers");

            migrationBuilder.DropTable(
                name: "RoleToAsset");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "OrganizationProfiles");
        }
    }
}
