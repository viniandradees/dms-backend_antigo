using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManagementService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initital_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supplement_dosage_reference");

            migrationBuilder.DropColumn(
                name: "side_effects",
                table: "supplement");

            migrationBuilder.AlterColumn<string>(
                name: "precautions",
                table: "supplement",
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
                table: "supplement",
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
                table: "supplement",
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
                name: "supplement_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    supplement_id = table.Column<int>(type: "int", nullable: false),
                    disease_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplement_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_supplement_disease_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_supplement_disease_supplement_supplement_id",
                        column: x => x.supplement_id,
                        principalTable: "supplement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_supplement_disease_disease_id",
                table: "supplement_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplement_disease_supplement_id",
                table: "supplement_disease",
                column: "supplement_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supplement_disease");

            migrationBuilder.AlterColumn<string>(
                name: "precautions",
                table: "supplement",
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
                table: "supplement",
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
                table: "supplement",
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
                table: "supplement",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.CreateTable(
                name: "supplement_dosage_reference",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    supplement_id = table.Column<int>(type: "int", nullable: false),
                    children_dosage_min = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    children_dosage_max = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    children_max_period = table.Column<int>(type: "int", nullable: false),
                    adult_dosage_min = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    adult_dosage_max = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    adult_max_period = table.Column<int>(type: "int", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplement_dosage_reference", x => x.id);
                    table.ForeignKey(
                        name: "FK_supplement_dosage_reference_measurement_unit_measurement_uni~",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_supplement_dosage_reference_supplement_supplement_id",
                        column: x => x.supplement_id,
                        principalTable: "supplement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "supplement",
                keyColumn: "id",
                keyValue: 1,
                column: "side_effects",
                value: "May cause nausea and headache at high doses.");

            migrationBuilder.UpdateData(
                table: "supplement",
                keyColumn: "id",
                keyValue: 2,
                column: "side_effects",
                value: "May cause fishy aftertaste and gastrointestinal discomfort.");

            migrationBuilder.UpdateData(
                table: "supplement",
                keyColumn: "id",
                keyValue: 3,
                column: "side_effects",
                value: "May cause bloating and gas initially.");

            migrationBuilder.UpdateData(
                table: "supplement",
                keyColumn: "id",
                keyValue: 4,
                column: "side_effects",
                value: "May cause constipation and stomach upset.");

            migrationBuilder.UpdateData(
                table: "supplement",
                keyColumn: "id",
                keyValue: 5,
                column: "side_effects",
                value: "May cause diarrhea and gastrointestinal upset at high doses.");

            migrationBuilder.CreateIndex(
                name: "IX_supplement_dosage_reference_measurement_unit_id",
                table: "supplement_dosage_reference",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplement_dosage_reference_supplement_id",
                table: "supplement_dosage_reference",
                column: "supplement_id");
        }
    }
}
