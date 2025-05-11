using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exer4_2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AgentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Agents_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "AgentID", "Address", "AgentName" },
                values: new object[,]
                {
                    { 1, "10 Pine St", "Alice Smith" },
                    { 2, "22 Oak Ave", "Bob Johnson" },
                    { 3, "33 Elm Rd", "Charlie Brown" },
                    { 4, "44 Willow Ln", "Diana Lee" },
                    { 5, "55 Maple Dr", "Ethan Davis" },
                    { 6, "66 Birch Ct", "Fiona Wilson" },
                    { 7, "77 Cedar Blvd", "George Moore" },
                    { 8, "88 Spruce Way", "Hannah Taylor" },
                    { 9, "99 Redwood Pl", "Ian Clark" },
                    { 10, "101 Oak St", "Julia Lewis" },
                    { 11, "112 Pine Ave", "Kevin Hall" },
                    { 12, "123 Elm Rd", "Linda Allen" },
                    { 13, "134 Willow Ln", "Michael Young" },
                    { 14, "145 Maple Dr", "Nancy King" },
                    { 15, "156 Birch Ct", "Oliver Wright" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "ItemName", "Size", "Stock" },
                values: new object[,]
                {
                    { 1, "T-Shirt", "M", 50 },
                    { 2, "Jeans", "32", 30 },
                    { 3, "Mug", null, 100 },
                    { 4, "Book", null, 75 },
                    { 5, "Pen", null, 200 },
                    { 6, "Sweater", "S", 25 },
                    { 7, "Belt", "34", 40 },
                    { 8, "Backpack", null, 60 },
                    { 9, "Dress", "8", 35 },
                    { 10, "Notebook", null, 120 },
                    { 11, "Gloves", "M", 70 },
                    { 12, "Scarf", "One Size", 55 },
                    { 13, "Water Bottle", null, 90 },
                    { 14, "Pants", "30", 32 },
                    { 15, "Keyboard", null, 45 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "alice@example.com", "P@$$wOrd1", "alice123" },
                    { 2, "bob@example.org", "SecurePwd2!", "bob_the_builder" },
                    { 3, "charlie@test.net", "MySecret3#", "charlie77" },
                    { 4, "diana@sample.com", "StrongPass4$", "diana.lee" },
                    { 5, "ethan@code.com", "CodeMaster5%", "ethan_dev" },
                    { 6, "fiona@art.net", "ArtLover6^", "fiona_art" },
                    { 7, "george@science.org", "Science7&", "george_sci" },
                    { 8, "hannah@books.com", "BookWorm8*", "hannah_read" },
                    { 9, "ian@games.net", "LevelUp9(", "ian_gamer" },
                    { 10, "julia@travel.org", "WorldTrip10)", "julia_travel" },
                    { 11, "kevin@music.com", "TuneTime11!", "kevin_music" },
                    { 12, "linda@bake.net", "SweetTreat12#", "linda_bake" },
                    { 13, "michael@photo.org", "SnapShot13$", "michael_photo" },
                    { 14, "nancy@garden.com", "GreenThumb14%", "nancy_garden" },
                    { 15, "oliver@tech.net", "FutureIsNow15^", "oliver_tech" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "AgentID", "OrderDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 8, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 8, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 8, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 8, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "ID", "ItemID", "OrderID", "Quantity", "UnitAmount" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 10 },
                    { 2, 2, 1, 2, 20 },
                    { 3, 3, 2, 1, 30 },
                    { 4, 4, 2, 3, 15 },
                    { 5, 5, 3, 1, 25 },
                    { 6, 6, 3, 2, 35 },
                    { 7, 7, 4, 1, 45 },
                    { 8, 8, 4, 4, 12 },
                    { 9, 9, 5, 1, 22 },
                    { 10, 10, 5, 2, 32 },
                    { 11, 11, 1, 1, 42 },
                    { 12, 12, 2, 3, 18 },
                    { 13, 13, 3, 1, 28 },
                    { 14, 14, 4, 2, 38 },
                    { 15, 15, 5, 1, 48 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemID",
                table: "OrderDetails",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AgentID",
                table: "Orders",
                column: "AgentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Agents");
        }
    }
}
