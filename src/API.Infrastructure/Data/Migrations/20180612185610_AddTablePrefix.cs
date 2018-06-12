using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Infrastructure.Data.Migrations
{
    public partial class AddTablePrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "API_AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_OpenIddictApplications",
                columns: table => new
                {
                    ClientId = table.Column<string>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    ConsentType = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Permissions = table.Column<string>(nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    RedirectUris = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_OpenIddictScopes",
                columns: table => new
                {
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    Resources = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_ReportTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    ThumbnailImage = table.Column<string>(nullable: true),
                    FullSizeImage = table.Column<string>(nullable: true),
                    PdfFile = table.Column<string>(nullable: true),
                    ZipFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_ReportTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_ReportTemplateTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_ReportTemplateTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_AspNetRoleClaims_API_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "API_AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_AspNetUserClaims_API_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "API_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_API_AspNetUserLogins_API_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "API_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_API_AspNetUserRoles_API_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "API_AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_API_AspNetUserRoles_API_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "API_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_API_AspNetUserTokens_API_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "API_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_OpenIddictAuthorizations",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    Scopes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_OpenIddictAuthorizations_API_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "API_OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "API_ReportTemplateReportTemplateTags",
                columns: table => new
                {
                    ReportTemplateId = table.Column<int>(nullable: false),
                    ReportTemplateTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_ReportTemplateReportTemplateTags", x => new { x.ReportTemplateId, x.ReportTemplateTagId });
                    table.ForeignKey(
                        name: "FK_API_ReportTemplateReportTemplateTags_API_ReportTemplates_ReportTemplateId",
                        column: x => x.ReportTemplateId,
                        principalTable: "API_ReportTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_API_ReportTemplateReportTemplateTags_API_ReportTemplateTags_ReportTemplateTagId",
                        column: x => x.ReportTemplateTagId,
                        principalTable: "API_ReportTemplateTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_OpenIddictTokens",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(nullable: true),
                    AuthorizationId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    ReferenceId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_OpenIddictTokens_API_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "API_OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_API_OpenIddictTokens_API_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "API_OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateTags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Scenario" },
                    { 2, "Count" },
                    { 3, "Status" },
                    { 4, "Unused" }
                });

            migrationBuilder.InsertData(
                table: "API_ReportTemplates",
                columns: new[] { "Id", "CreationDate", "Description", "FullSizeImage", "LastModifiedDate", "Name", "PdfFile", "ThumbnailImage", "ZipFile" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The test execution result by test scenario", "/Sample-data/TestExecutionResultByTestScenario/image.png", new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Execution Result By Test Scenario", "/Sample-data/TestExecutionResultByTestScenario/TestExecutionResultByTestScenario.pdf", "/Sample-data/TestExecutionResultByTestScenario/thumbnail.png", "/Sample-data/TestExecutionResultByTestScenario/TestExecutionResultByTestScenario.zip" },
                    { 2, new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The total count of defects on each day sorted by status", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/image.png", new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Total Count Of Defects On Each Day Sorted By Status", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/TotalCountOfDefectsOnEachDaySortedByStatus.pdf", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/thumbnail.png", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/TotalCountOfDefectsOnEachDaySortedByStatus.zip" }
                });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateReportTemplateTags",
                columns: new[] { "ReportTemplateId", "ReportTemplateTagId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateReportTemplateTags",
                columns: new[] { "ReportTemplateId", "ReportTemplateTagId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateReportTemplateTags",
                columns: new[] { "ReportTemplateId", "ReportTemplateTagId" },
                values: new object[] { 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_API_AspNetRoleClaims_RoleId",
                table: "API_AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "API_AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_API_AspNetUserClaims_UserId",
                table: "API_AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_API_AspNetUserLogins_UserId",
                table: "API_AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_API_AspNetUserRoles_RoleId",
                table: "API_AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "API_AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "API_AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_API_OpenIddictApplications_ClientId",
                table: "API_OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_API_OpenIddictAuthorizations_ApplicationId",
                table: "API_OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_API_OpenIddictScopes_Name",
                table: "API_OpenIddictScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_API_OpenIddictTokens_ApplicationId",
                table: "API_OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_API_OpenIddictTokens_AuthorizationId",
                table: "API_OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_API_OpenIddictTokens_ReferenceId",
                table: "API_OpenIddictTokens",
                column: "ReferenceId",
                unique: true,
                filter: "[ReferenceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_API_ReportTemplateReportTemplateTags_ReportTemplateTagId",
                table: "API_ReportTemplateReportTemplateTags",
                column: "ReportTemplateTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "API_AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "API_AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "API_AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "API_AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "API_AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "API_OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "API_OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "API_ReportTemplateReportTemplateTags");

            migrationBuilder.DropTable(
                name: "API_AspNetRoles");

            migrationBuilder.DropTable(
                name: "API_AspNetUsers");

            migrationBuilder.DropTable(
                name: "API_OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "API_ReportTemplates");

            migrationBuilder.DropTable(
                name: "API_ReportTemplateTags");

            migrationBuilder.DropTable(
                name: "API_OpenIddictApplications");
        }
    }
}
