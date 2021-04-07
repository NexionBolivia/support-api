using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Support.API.Services.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
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
                    table.PrimaryKey("PK_Asset", x => x.AssetId);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProfile",
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
                    table.PrimaryKey("PK_OrganizationProfile", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Color = table.Column<string>(maxLength: 50, nullable: true),
                    IdProfile = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organization_OrganizationProfile_IdProfile",
                        column: x => x.IdProfile,
                        principalTable: "OrganizationProfile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_RoleToAsset_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleToAsset_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleToKoboUser",
                columns: table => new
                {
                    KoboUserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleToKoboUser", x => new { x.KoboUserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleToKoboUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationToKoboUser",
                columns: table => new
                {
                    KoboUserId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationToKoboUser", x => new { x.KoboUserId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_OrganizationToKoboUser_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organization_IdProfile",
                table: "Organization",
                column: "IdProfile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationToKoboUser_OrganizationId",
                table: "OrganizationToKoboUser",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleToAsset_AssetId",
                table: "RoleToAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleToKoboUser_RoleId",
                table: "RoleToKoboUser",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationToKoboUser");

            migrationBuilder.DropTable(
                name: "RoleToAsset");

            migrationBuilder.DropTable(
                name: "RoleToKoboUser");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "OrganizationProfile");
        }
    }
}
