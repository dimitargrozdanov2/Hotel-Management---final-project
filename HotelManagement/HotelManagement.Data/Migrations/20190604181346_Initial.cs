using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelManagement.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
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
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Comment = table.Column<string>(maxLength: 200, nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: true),
                    BusinessId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    BusinessId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logbooks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    BusinessId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logbooks_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogbookManagers",
                columns: table => new
                {
                    LogbookId = table.Column<string>(nullable: false),
                    ManagerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogbookManagers", x => new { x.ManagerId, x.LogbookId });
                    table.ForeignKey(
                        name: "FK_LogbookManagers_Logbooks_LogbookId",
                        column: x => x.LogbookId,
                        principalTable: "Logbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogbookManagers_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Text = table.Column<string>(maxLength: 200, nullable: false),
                    LogbookId = table.Column<string>(nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    PriorityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_Logbooks_LogbookId",
                        column: x => x.LogbookId,
                        principalTable: "Logbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e78767e-b4d2-456f-b029-5ad4c454589a", "6e63ab39-a514-4e99-ba86-296cfe8d15b5", "Admin", "ADMIN" },
                    { "ca8a9288-1bda-4320-8929-731d721be047", "190ecb23-4911-48ee-bf11-62b3157eb20b", "Manager", "MANAGER" },
                    { "2c9e83b9-1015-42b6-9df5-ad54b3d98224", "846eb2cf-9cce-4b54-99db-f87cbde271df", "Moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6404c00f-c0e6-4a92-ad71-43b24f5f0e97", 0, "b318fa87-5d53-4bc3-8019-23d7aa37da20", "admin@admin.admin", false, false, null, "ADMIN@ADMIN.ADMIN", "ADMIN@ADMIN.ADMIN", "AQAAAAEAACcQAAAAEG5yDdPexa5KQHAhorczw165qs8mxitFXgC5YGEA1XRuewjCiAB4VDHSpuWZ1oY/Ow==", null, false, "7I2NUNAXILZUAHNGX7TRSNQCNRWCEOSX", false, "admin@admin.admin" });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "CreatedOn", "Description", "IsDeleted", "Location", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { "14f77522-b07f-4ad8-855b-d93923bea56e", new DateTime(2019, 5, 3, 17, 10, 20, 0, DateTimeKind.Unspecified), "Located in the bustling Dubai Marina neighbourhood, Rove Dubai Marina is the perfect location to start your next adventure.", false, "United Arab Emirates, Dubai", null, "Rove Dubai Marina" },
                    { "687af33b-3084-43b6-bacb-4c8847559ee4", new DateTime(2019, 4, 3, 15, 20, 11, 0, DateTimeKind.Unspecified), "This hotel is located on a front of the Palm Islands. Perfect for honeymoons, this hotel has 64 rooms, 26 suites, and four Moorish-style villas.", false, "United Arab Emirates, Dubai", null, "The Palm" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { "450b6f6e-95b3-400d-a258-aafc6b6ecd07", new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified), false, null, "TODO" },
                    { "b73de489-d731-4f3a-8be7-a6613fe5eb6f", new DateTime(2019, 5, 2, 15, 26, 10, 0, DateTimeKind.Unspecified), false, null, "Maintenance" },
                    { "5c97553f-1557-4352-9622-d5bdd37f44f4", new DateTime(2019, 5, 3, 18, 45, 23, 0, DateTimeKind.Unspecified), false, null, "Events" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "6404c00f-c0e6-4a92-ad71-43b24f5f0e97", "0e78767e-b4d2-456f-b029-5ad4c454589a" },
                    { "6404c00f-c0e6-4a92-ad71-43b24f5f0e97", "ca8a9288-1bda-4320-8929-731d721be047" },
                    { "6404c00f-c0e6-4a92-ad71-43b24f5f0e97", "2c9e83b9-1015-42b6-9df5-ad54b3d98224" }
                });

            migrationBuilder.InsertData(
                table: "Feedback",
                columns: new[] { "Id", "BusinessId", "Comment", "CreatedOn", "IsDeleted", "ModifiedOn", "Name", "Number", "Rating" },
                values: new object[,]
                {
                    { "1e67e958-37fc-46cc-a6b9-5d1f28e9e532", "14f77522-b07f-4ad8-855b-d93923bea56e", "We had a wonderful stay! The staff could not have been more helpful!", new DateTime(2019, 5, 2, 15, 26, 10, 0, DateTimeKind.Unspecified), false, null, "Scarlett Johansson", "+359 893 92 00 55", 3.0 },
                    { "d2dd7ddf-ac78-42f1-963a-16f06406bd9d", "14f77522-b07f-4ad8-855b-d93923bea56e", "The hotel is in a very good location, visited the restaurant and had a really great time!", new DateTime(2019, 5, 3, 18, 45, 23, 0, DateTimeKind.Unspecified), false, null, "Sandra Bullock", "+359 898 11 23 44", 1.0 },
                    { "b4f2f3c8-a725-44a0-8fd6-651ee45a9690", "687af33b-3084-43b6-bacb-4c8847559ee4", "Great location, really clean rooms, awesome staff!", new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified), false, null, "Jeff Goldblum", "+359 896 71 99 88", 5.0 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "BusinessId", "CreatedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { "0f34be57-d215-43fb-b442-45f246b651d5", "14f77522-b07f-4ad8-855b-d93923bea56e", new DateTime(2019, 3, 4, 15, 36, 5, 0, DateTimeKind.Unspecified), false, null, "Rove Dubai Marina_logo.jpg" },
                    { "c8cae9c7-d536-4049-ad77-8324187fbc83", "14f77522-b07f-4ad8-855b-d93923bea56e", new DateTime(2019, 3, 4, 15, 36, 5, 0, DateTimeKind.Unspecified), false, null, "Rove Dubai Marina_Restaurant.jpg" },
                    { "55973f04-9f96-45e0-8ea2-3ceb0fb7ce5e", "14f77522-b07f-4ad8-855b-d93923bea56e", new DateTime(2019, 3, 4, 15, 36, 5, 0, DateTimeKind.Unspecified), false, null, "Rove Dubai Marina_Exclusive Lounge.jpg" },
                    { "83705aa7-d523-4978-b799-84e15468a088", "687af33b-3084-43b6-bacb-4c8847559ee4", new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified), false, null, "The Palm_logo.jpg" },
                    { "93a31b5d-671b-42bb-a457-bd60b5f124e3", "687af33b-3084-43b6-bacb-4c8847559ee4", new DateTime(2019, 3, 4, 15, 36, 5, 0, DateTimeKind.Unspecified), false, null, "The Palm_Restaurant.jpg" },
                    { "0d07b40a-ef2b-4c5e-99ed-5ead465b6b1e", "687af33b-3084-43b6-bacb-4c8847559ee4", new DateTime(2019, 3, 4, 15, 36, 5, 0, DateTimeKind.Unspecified), false, null, "The Palm_Bar.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Logbooks",
                columns: new[] { "Id", "BusinessId", "CreatedOn", "Description", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { "3d71d939-dc61-46f8-af46-ed6a618036c2", "14f77522-b07f-4ad8-855b-d93923bea56e", new DateTime(2019, 5, 4, 14, 40, 5, 0, DateTimeKind.Unspecified), "Deliciously international cuisine awaits you at our on-site restaurant, Mosaic Restaurant & Terrace, offering an adventurous array of dishes from Egypt, Lebanon, Russia, India and China and beyond!", false, null, "Restaurant" },
                    { "1fc92a85-06de-4c97-9230-295d4d2b445c", "14f77522-b07f-4ad8-855b-d93923bea56e", new DateTime(2019, 5, 4, 14, 41, 5, 0, DateTimeKind.Unspecified), "Spectacular views, awesome place to meet friends and relax. Check out our creative and refreshing drinks. The Exclusive Lounge is restricted to our V.I.P guests, feel free to request acccess anytime.", false, null, "Exclusive Lounge" },
                    { "834c3420-56d9-4dbc-900b-c792cb15be83", "687af33b-3084-43b6-bacb-4c8847559ee4", new DateTime(2019, 3, 4, 16, 21, 10, 0, DateTimeKind.Unspecified), "Deliciously international cuisine awaits you at our on-site restaurant, Mosaic Restaurant & Terrace, offering an adventurous array of dishes from Egypt, Lebanon, Russia, India and China and beyond!", false, null, "Restaurant" },
                    { "cc9ea717-1788-49d8-9a3d-bd0bc3eb73ae", "687af33b-3084-43b6-bacb-4c8847559ee4", new DateTime(2019, 3, 4, 16, 20, 10, 0, DateTimeKind.Unspecified), "Amazing views, perfect place to meet friends and relax. Check out our latest collection of summer drinks. Our bar is open 24/7, hosting the best parties in Dubai, every night, every day! Visit us!", false, null, "Bar" }
                });

            migrationBuilder.InsertData(
                table: "LogbookManagers",
                columns: new[] { "ManagerId", "LogbookId" },
                values: new object[] { "6404c00f-c0e6-4a92-ad71-43b24f5f0e97", "3d71d939-dc61-46f8-af46-ed6a618036c2" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "IsDeleted", "LogbookId", "ModifiedOn", "PriorityType", "Text", "UserId" },
                values: new object[] { "f7257688-84ea-4327-8841-ac78f3e8d2f6", "450b6f6e-95b3-400d-a258-aafc6b6ecd07", new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified), false, "3d71d939-dc61-46f8-af46-ed6a618036c2", null, 1, "Check reception documents!", "6404c00f-c0e6-4a92-ad71-43b24f5f0e97" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_BusinessId",
                table: "Feedback",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BusinessId",
                table: "Images",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_LogbookManagers_LogbookId",
                table: "LogbookManagers",
                column: "LogbookId");

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_BusinessId",
                table: "Logbooks",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_LogbookId",
                table: "Notes",
                column: "LogbookId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "LogbookManagers");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Logbooks");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
