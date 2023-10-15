using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManagementService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initital_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "food_attribute_incompatibility");

            migrationBuilder.DropTable(
                name: "meal_cuisine_type");

            migrationBuilder.DropTable(
                name: "meal_cuisine_description");

            migrationBuilder.AddColumn<int>(
                name: "food_id",
                table: "meal_food",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateTable(
                name: "dietary_option",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dietary_option", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meal_country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    meal_id = table.Column<int>(type: "int", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_country", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_country_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_country_meal_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dietary_option_food_attribute",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dietary_option_id = table.Column<int>(type: "int", nullable: false),
                    food_attribute_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dietary_option_food_attribute", x => x.id);
                    table.ForeignKey(
                        name: "FK_dietary_option_food_attribute_dietary_option_dietary_option_~",
                        column: x => x.dietary_option_id,
                        principalTable: "dietary_option",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dietary_option_food_attribute_food_attribute_food_attribute_~",
                        column: x => x.food_attribute_id,
                        principalTable: "food_attribute",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meal_dietary_option",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    meal_id = table.Column<int>(type: "int", nullable: false),
                    dietary_option_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_dietary_option", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_dietary_option_dietary_option_dietary_option_id",
                        column: x => x.dietary_option_id,
                        principalTable: "dietary_option",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_dietary_option_meal_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_meal_food_food_id",
                table: "meal_food",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_dietary_option_name",
                table: "dietary_option",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dietary_option_food_attribute_dietary_option_id",
                table: "dietary_option_food_attribute",
                column: "dietary_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_dietary_option_food_attribute_food_attribute_id",
                table: "dietary_option_food_attribute",
                column: "food_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_country_country_id",
                table: "meal_country",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_country_meal_id",
                table: "meal_country",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_dietary_option_dietary_option_id",
                table: "meal_dietary_option",
                column: "dietary_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_dietary_option_meal_id",
                table: "meal_dietary_option",
                column: "meal_id");

            migrationBuilder.AddForeignKey(
                name: "FK_meal_food_food_food_id",
                table: "meal_food",
                column: "food_id",
                principalTable: "food",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meal_food_food_food_id",
                table: "meal_food");

            migrationBuilder.DropTable(
                name: "dietary_option_food_attribute");

            migrationBuilder.DropTable(
                name: "meal_country");

            migrationBuilder.DropTable(
                name: "meal_dietary_option");

            migrationBuilder.DropTable(
                name: "dietary_option");

            migrationBuilder.DropIndex(
                name: "IX_meal_food_food_id",
                table: "meal_food");

            migrationBuilder.DropColumn(
                name: "food_id",
                table: "meal_food");

            migrationBuilder.CreateTable(
                name: "food_attribute_incompatibility",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    food_attribute_id = table.Column<int>(type: "int", nullable: false),
                    incompatible_food_attribute_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_attribute_incompatibility", x => x.id);
                    table.ForeignKey(
                        name: "FK_food_attribute_incompatibility_food_attribute_food_attribute~",
                        column: x => x.food_attribute_id,
                        principalTable: "food_attribute",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_attribute_incompatibility_food_attribute_incompatible_f~",
                        column: x => x.incompatible_food_attribute_id,
                        principalTable: "food_attribute",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meal_cuisine_description",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_cuisine_description", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meal_cuisine_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    meal_id = table.Column<int>(type: "int", nullable: false),
                    meal_cuisine_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_cuisine_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_cuisine_type_meal_cuisine_description_meal_cuisine_type~",
                        column: x => x.meal_cuisine_type_id,
                        principalTable: "meal_cuisine_description",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_cuisine_type_meal_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_food_attribute_incompatibility_food_attribute_id",
                table: "food_attribute_incompatibility",
                column: "food_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_attribute_incompatibility_incompatible_food_attribute_id",
                table: "food_attribute_incompatibility",
                column: "incompatible_food_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_cuisine_type_meal_cuisine_type_id",
                table: "meal_cuisine_type",
                column: "meal_cuisine_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_cuisine_type_meal_id",
                table: "meal_cuisine_type",
                column: "meal_id");
        }
    }
}
