using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dish", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<sbyte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsApproved = table.Column<sbyte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "menu_dish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Menu = table.Column<int>(type: "int", nullable: false),
                    Dish = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_dish", x => x.Id);
                    table.ForeignKey(
                        name: "dish",
                        column: x => x.Dish,
                        principalTable: "dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "menu",
                        column: x => x.Menu,
                        principalTable: "menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "record",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsReady = table.Column<sbyte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_record", x => x.Id);
                    table.ForeignKey(
                        name: "userFK",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "admin",
                        column: x => x.Admin,
                        principalTable: "user",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "user",
                        column: x => x.User,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "record_dish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Dish = table.Column<int>(type: "int", nullable: false),
                    Record = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_record_dish", x => x.Id);
                    table.ForeignKey(
                        name: "dishId",
                        column: x => x.Dish,
                        principalTable: "dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "recordId",
                        column: x => x.Record,
                        principalTable: "record",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.InsertData(
                table: "dish",
                columns: new[] { "Id", "Name", "Position", "Price" },
                values: new object[,]
                {
                    { 1, "Первое блюдо", 1, 0m },
                    { 2, "Второе блюдо", 2, 0m },
                    { 3, "Комплексный обед", 3, 0m },
                    { 4, "Борщ", 1, 200m },
                    { 5, "Окрошка", 1, 200m },
                    { 6, "Солянка", 1, 220m },
                    { 7, "Щи из свежей капустой", 1, 200m },
                    { 8, "Рассольник", 1, 200m },
                    { 9, "Уха", 1, 220m },
                    { 10, "Лапша с курицей", 1, 180m },
                    { 11, "Паста Карбонара", 2, 250m },
                    { 12, "Пюре с котлетой", 2, 225m },
                    { 13, "Хинкали", 2, 230m },
                    { 14, "Макароны с курицей", 2, 180m }
                });

            migrationBuilder.InsertData(
                table: "menu",
                columns: new[] { "Id", "Date", "IsActive" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)1 },
                    { 2, new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)1 },
                    { 3, new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)1 },
                    { 4, new DateTime(2022, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)1 },
                    { 5, new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)1 },
                    { 6, new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)0 },
                    { 7, new DateTime(2022, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)0 }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "Email", "IsApproved", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "dinneradmin@gmail.com", (sbyte)0, "Виталий Волков", "qwerty_Admin", "admin" },
                    { 2, "dinnercook@gmail.com", (sbyte)0, "Вова Вист", "asdf_Cook", "cook" },
                    { 3, "casualuser@gmail.com", (sbyte)0, "Алекс Дарксталкер98", "devkabezruki", "user" }
                });

            migrationBuilder.InsertData(
                table: "menu_dish",
                columns: new[] { "Id", "Dish", "Menu" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 8, 1 },
                    { 3, 2, 2 },
                    { 4, 9, 2 },
                    { 5, 3, 3 },
                    { 6, 10, 3 },
                    { 7, 4, 4 },
                    { 8, 11, 4 },
                    { 9, 5, 5 },
                    { 10, 8, 5 }
                });

            migrationBuilder.InsertData(
                table: "record",
                columns: new[] { "Id", "Date", "IsReady", "Price", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)0, 450m, 3 },
                    { 2, new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), (sbyte)0, 225m, 3 }
                });

            migrationBuilder.InsertData(
                table: "transaction",
                columns: new[] { "Id", "Admin", "Date", "Price", "User" },
                values: new object[] { 1, 1, new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 225m, 3 });

            migrationBuilder.InsertData(
                table: "record_dish",
                columns: new[] { "Id", "Dish", "Record" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "record_dish",
                columns: new[] { "Id", "Dish", "Record" },
                values: new object[] { 2, 8, 1 });

            migrationBuilder.InsertData(
                table: "record_dish",
                columns: new[] { "Id", "Dish", "Record" },
                values: new object[] { 3, 9, 2 });

            migrationBuilder.CreateIndex(
                name: "dish_idx",
                table: "menu_dish",
                column: "Dish");

            migrationBuilder.CreateIndex(
                name: "menu_idx",
                table: "menu_dish",
                column: "Menu");

            migrationBuilder.CreateIndex(
                name: "user_idx",
                table: "record",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "dish_idx1",
                table: "record_dish",
                column: "Dish");

            migrationBuilder.CreateIndex(
                name: "recordId_idx",
                table: "record_dish",
                column: "Record");

            migrationBuilder.CreateIndex(
                name: "admin_idx",
                table: "transaction",
                column: "Admin");

            migrationBuilder.CreateIndex(
                name: "user_idx1",
                table: "transaction",
                column: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_dish");

            migrationBuilder.DropTable(
                name: "record_dish");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "dish");

            migrationBuilder.DropTable(
                name: "record");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
