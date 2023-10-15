using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManagementService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initital_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "side_effects",
                table: "lifestyle");

            migrationBuilder.DropColumn(
                name: "side_effects",
                table: "food");

            migrationBuilder.AlterColumn<string>(
                name: "precautions",
                table: "lifestyle",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "interactions",
                table: "lifestyle",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "precautions",
                table: "food",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "interactions",
                table: "food",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "best_time_to_take",
                table: "food",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.CreateTable(
                name: "food_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    disease_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_food_disease_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_disease_food_food_id",
                        column: x => x.food_id,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lifestyle_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    lifestyle_id = table.Column<int>(type: "int", nullable: false),
                    disease_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lifestyle_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_lifestyle_disease_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lifestyle_disease_lifestyle_lifestyle_id",
                        column: x => x.lifestyle_id,
                        principalTable: "lifestyle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_food_disease_disease_id",
                table: "food_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_disease_food_id",
                table: "food_disease",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_lifestyle_disease_disease_id",
                table: "lifestyle_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_lifestyle_disease_lifestyle_id",
                table: "lifestyle_disease",
                column: "lifestyle_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "food_disease");

            migrationBuilder.DropTable(
                name: "lifestyle_disease");

            migrationBuilder.AlterColumn<string>(
                name: "precautions",
                table: "lifestyle",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "interactions",
                table: "lifestyle",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<string>(
                name: "side_effects",
                table: "lifestyle",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "precautions",
                table: "food",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "interactions",
                table: "food",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "best_time_to_take",
                table: "food",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<string>(
                name: "side_effects",
                table: "food",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.UpdateData(
                table: "food",
                keyColumn: "id",
                keyValue: 1,
                column: "side_effects",
                value: "Rarely, some individuals may experience allergic reactions.");

            migrationBuilder.UpdateData(
                table: "food",
                keyColumn: "id",
                keyValue: 2,
                column: "side_effects",
                value: "May cause gas and bloating in some individuals.");

            migrationBuilder.UpdateData(
                table: "food",
                keyColumn: "id",
                keyValue: 3,
                column: "side_effects",
                value: "Generally well-tolerated, but may cause digestive upset in some people.");

            migrationBuilder.UpdateData(
                table: "food",
                keyColumn: "id",
                keyValue: 4,
                column: "side_effects",
                value: "Some individuals may have allergies to tree nuts.");

            migrationBuilder.UpdateData(
                table: "food",
                keyColumn: "id",
                keyValue: 5,
                column: "side_effects",
                value: "High oxalate content may contribute to kidney stone formation.");

            migrationBuilder.UpdateData(
                table: "lifestyle",
                keyColumn: "id",
                keyValue: 1,
                column: "side_effects",
                value: "Overtraining may lead to injuries and burnout.");

            migrationBuilder.UpdateData(
                table: "lifestyle",
                keyColumn: "id",
                keyValue: 2,
                column: "side_effects",
                value: "Improper diet may lead to nutrient deficiencies and health issues.");

            migrationBuilder.UpdateData(
                table: "lifestyle",
                keyColumn: "id",
                keyValue: 3,
                column: "side_effects",
                value: "Chronic stress can contribute to various health problems.");

            migrationBuilder.UpdateData(
                table: "lifestyle",
                keyColumn: "id",
                keyValue: 4,
                column: "side_effects",
                value: "Sleep deprivation may lead to fatigue, mood changes, and impaired cognition.");

            migrationBuilder.UpdateData(
                table: "lifestyle",
                keyColumn: "id",
                keyValue: 5,
                column: "side_effects",
                value: "Tobacco use is a major cause of lung disease, cancer, and heart disease.");
        }
    }
}
