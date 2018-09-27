using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualPet.Application.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    MoodTimeModifier = table.Column<int>(nullable: false),
                    HungerTimeModifier = table.Column<int>(nullable: false),
                    StrokeModifier = table.Column<int>(nullable: false),
                    FeedModifier = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Mood = table.Column<int>(nullable: false, defaultValue: 50),
                    Hunger = table.Column<int>(nullable: false, defaultValue: 50),
                    Name = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    PetProfileId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    PetTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Profiles_PetProfileId",
                        column: x => x.PetProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_PetTypes_PetTypeId",
                        column: x => x.PetTypeId,
                        principalTable: "PetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    PetId = table.Column<int>(nullable: false),
                    EventTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "CreateDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), "The pet was born", "Born" },
                    { 2, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), "The pet was stroked", "Stroked" },
                    { 3, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), "The pet was fed", "Fed" }
                });

            migrationBuilder.InsertData(
                table: "PetTypes",
                columns: new[] { "Id", "CreateDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), null, "Dog" },
                    { 2, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), null, "Cat" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "CreateDate", "FeedModifier", "HungerTimeModifier", "MoodTimeModifier", "StrokeModifier" },
                values: new object[] { 1, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), 10, 1, 1, 10 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate" },
                values: new object[] { 1, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "CreateDate", "Hunger", "LastUpdated", "Mood", "Name", "PetProfileId", "PetTypeId", "UserId" },
                values: new object[] { 1, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), 50, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), 50, "Fido", 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreateDate", "EventTypeId", "PetId" },
                values: new object[] { 1, new DateTime(2018, 9, 26, 12, 48, 26, 442, DateTimeKind.Local), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Events_PetId",
                table: "Events",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetProfileId",
                table: "Pets",
                column: "PetProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetTypeId",
                table: "Pets",
                column: "PetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "PetTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
