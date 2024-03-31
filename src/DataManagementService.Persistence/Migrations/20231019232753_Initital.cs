using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataManagementService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    first_name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acronym = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nationality = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    continent = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    most_indicated_treatment = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "drug",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    interactions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precautions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    best_time_to_take = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drug", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prerequisite = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "food",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    interactions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precautions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    best_time_to_take = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "food_attribute",
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
                    table.PrimaryKey("PK_food_attribute", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "healty_objective",
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
                    table.PrimaryKey("PK_healty_objective", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lifestyle",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    interactions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precautions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lifestyle", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    preparation_method = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_calories = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "measurement_unit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acronym = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measurement_unit", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "supplement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    interactions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precautions = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    best_time_to_take = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplement", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_profile = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_details_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_id = table.Column<int>(type: "int", nullable: false),
                    symptom_id = table.Column<int>(type: "int", nullable: false),
                    symptom_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_disease_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_disease_disease_symptom_id",
                        column: x => x.symptom_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_drug",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_id = table.Column<int>(type: "int", nullable: false),
                    drug_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_drug", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_drug_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_drug_drug_drug_id",
                        column: x => x.drug_id,
                        principalTable: "drug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "drug_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    drug_id = table.Column<int>(type: "int", nullable: false),
                    disease_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drug_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_drug_disease_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_drug_disease_drug_drug_id",
                        column: x => x.drug_id,
                        principalTable: "drug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_exam",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_id = table.Column<int>(type: "int", nullable: false),
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    exam_result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_exam", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_exam_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_exam_exam_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_food",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_id = table.Column<int>(type: "int", nullable: false),
                    food_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_food", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_food_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_food_food_food_id",
                        column: x => x.food_id,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_food",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    exam_result = table.Column<int>(type: "int", nullable: false),
                    action = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_food", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_food_exam_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exam_food_food_food_id",
                        column: x => x.food_id,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "food_related_attribute",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    food_attribute_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_related_attribute", x => x.id);
                    table.ForeignKey(
                        name: "FK_food_related_attribute_food_attribute_food_attribute_id",
                        column: x => x.food_attribute_id,
                        principalTable: "food_attribute",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_related_attribute_food_food_id",
                        column: x => x.food_id,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "food_healty_objective",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    healty_objective_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_healty_objective", x => x.id);
                    table.ForeignKey(
                        name: "FK_food_healty_objective_food_food_id",
                        column: x => x.food_id,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_healty_objective_healty_objective_healty_objective_id",
                        column: x => x.healty_objective_id,
                        principalTable: "healty_objective",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_lifestyle",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_id = table.Column<int>(type: "int", nullable: false),
                    lifestyle_id = table.Column<int>(type: "int", nullable: false),
                    patient_action = table.Column<int>(type: "int", nullable: false),
                    patient_action_intensity = table.Column<int>(type: "int", nullable: false),
                    more_details = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_lifestyle", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_lifestyle_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_lifestyle_lifestyle_lifestyle_id",
                        column: x => x.lifestyle_id,
                        principalTable: "lifestyle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_lifestyle",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    lifestyle_id = table.Column<int>(type: "int", nullable: false),
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    exam_result = table.Column<int>(type: "int", nullable: false),
                    patient_action = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_lifestyle", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_lifestyle_exam_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exam_lifestyle_lifestyle_lifestyle_id",
                        column: x => x.lifestyle_id,
                        principalTable: "lifestyle",
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

            migrationBuilder.CreateTable(
                name: "meal_period",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    meal_id = table.Column<int>(type: "int", nullable: false),
                    meal_period_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_period", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_period_meal_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_result_reference",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false),
                    minimum_reference = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    maximum_reference = table.Column<decimal>(type: "decimal(11,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_result_reference", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_result_reference_exam_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exam_result_reference_measurement_unit_measurement_unit_id",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meal_food",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    meal_id = table.Column<int>(type: "int", nullable: false),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    food_portion = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_food", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_food_food_food_id",
                        column: x => x.food_id,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_food_meal_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_food_measurement_unit_measurement_unit_id",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_supplement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_id = table.Column<int>(type: "int", nullable: false),
                    supplement_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_supplement", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_supplement_disease_disease_id",
                        column: x => x.disease_id,
                        principalTable: "disease",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_supplement_supplement_supplement_id",
                        column: x => x.supplement_id,
                        principalTable: "supplement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_supplement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    supplement_id = table.Column<int>(type: "int", nullable: false),
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    exam_result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_supplement", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_supplement_exam_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exam_supplement_supplement_supplement_id",
                        column: x => x.supplement_id,
                        principalTable: "supplement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "food_supplement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    supplement_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_supplement", x => x.id);
                    table.ForeignKey(
                        name: "FK_food_supplement_food_food_id",
                        column: x => x.food_id,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_supplement_measurement_unit_measurement_unit_id",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_supplement_supplement_supplement_id",
                        column: x => x.supplement_id,
                        principalTable: "supplement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "disease_drug_dosage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_drug_id = table.Column<int>(type: "int", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false),
                    best_time_to_take = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    more_details = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_drug_dosage", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_drug_dosage_disease_drug_disease_drug_id",
                        column: x => x.disease_drug_id,
                        principalTable: "disease_drug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_drug_dosage_measurement_unit_measurement_unit_id",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_food_dosage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_food_id = table.Column<int>(type: "int", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false),
                    best_time_to_take = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    more_details = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_food_dosage", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_food_dosage_disease_food_disease_food_id",
                        column: x => x.disease_food_id,
                        principalTable: "disease_food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_food_dosage_measurement_unit_measurement_unit_id",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_result_reference_country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    exam_result_reference_id = table.Column<int>(type: "int", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_result_reference_country", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_result_reference_country_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exam_result_reference_country_exam_result_reference_exam_res~",
                        column: x => x.exam_result_reference_id,
                        principalTable: "exam_result_reference",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_result_reference_variation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    exam_result_reference_id = table.Column<int>(type: "int", nullable: false),
                    minimum_reference = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    maximum_reference = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    age_unit = table.Column<int>(type: "int", nullable: false),
                    patient_minimum_age = table.Column<int>(type: "int", nullable: false),
                    patient_maximum_age = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    pregnancy_required = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    menopause_required = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_result_reference_variation", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_result_reference_variation_exam_result_reference_exam_r~",
                        column: x => x.exam_result_reference_id,
                        principalTable: "exam_result_reference",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_supplement_dosage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_supplement_id = table.Column<int>(type: "int", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false),
                    best_time_to_take = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    more_details = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_supplement_dosage", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_supplement_dosage_disease_supplement_disease_supplem~",
                        column: x => x.disease_supplement_id,
                        principalTable: "disease_supplement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disease_supplement_dosage_measurement_unit_measurement_unit_~",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_supplement_dosage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    exam_supplement_id = table.Column<int>(type: "int", nullable: false),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false),
                    best_time_to_take = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    more_details = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_supplement_dosage", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_supplement_dosage_exam_supplement_exam_supplement_id",
                        column: x => x.exam_supplement_id,
                        principalTable: "exam_supplement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exam_supplement_dosage_measurement_unit_measurement_unit_id",
                        column: x => x.measurement_unit_id,
                        principalTable: "measurement_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_drug_dosage_age_range",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_drug_id = table.Column<int>(type: "int", nullable: false),
                    age_unit = table.Column<int>(type: "int", nullable: false, comment: "1=days; 2=months; 3=years"),
                    time_min = table.Column<int>(type: "int", nullable: false),
                    time_max = table.Column<int>(type: "int", nullable: false),
                    dosage_min = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    dosage_max = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    max_usage_period = table.Column<int>(type: "int", nullable: false, comment: "in days"),
                    recommended_first_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_second_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_third_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_fourth_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_drug_dosage_age_range", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_drug_dosage_age_range_disease_drug_dosage_disease_dr~",
                        column: x => x.disease_drug_id,
                        principalTable: "disease_drug_dosage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_food_dosage_age_range",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_food_id = table.Column<int>(type: "int", nullable: false),
                    age_unit = table.Column<int>(type: "int", nullable: false, comment: "1=days; 2=months; 3=years"),
                    time_min = table.Column<int>(type: "int", nullable: false),
                    time_max = table.Column<int>(type: "int", nullable: false),
                    dosage_min = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    dosage_max = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    max_usage_period = table.Column<int>(type: "int", nullable: false, comment: "in days"),
                    recommended_first_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_second_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_third_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_fourth_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_food_dosage_age_range", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_food_dosage_age_range_disease_food_dosage_disease_fo~",
                        column: x => x.disease_food_id,
                        principalTable: "disease_food_dosage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disease_supplement_dosage_age_range",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    disease_supplement_id = table.Column<int>(type: "int", nullable: false),
                    age_unit = table.Column<int>(type: "int", nullable: false, comment: "1=days; 2=months; 3=years"),
                    time_min = table.Column<int>(type: "int", nullable: false),
                    time_max = table.Column<int>(type: "int", nullable: false),
                    dosage_min = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    dosage_max = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    max_usage_period = table.Column<int>(type: "int", nullable: false, comment: "in days"),
                    recommended_first_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_second_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_third_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_fourth_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disease_supplement_dosage_age_range", x => x.id);
                    table.ForeignKey(
                        name: "FK_disease_supplement_dosage_age_range_disease_supplement_dosag~",
                        column: x => x.disease_supplement_id,
                        principalTable: "disease_supplement_dosage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exam_supplement_dosage_age_range",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    exam_supplement_id = table.Column<int>(type: "int", nullable: false),
                    age_unit = table.Column<int>(type: "int", nullable: false, comment: "1=days; 2=months; 3=years"),
                    time_min = table.Column<int>(type: "int", nullable: false),
                    time_max = table.Column<int>(type: "int", nullable: false),
                    dosage_min = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    dosage_max = table.Column<decimal>(type: "decimal(11,5)", nullable: false),
                    max_usage_period = table.Column<int>(type: "int", nullable: false, comment: "in days"),
                    recommended_first_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_second_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_third_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    recommended_fourth_quarter = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_supplement_dosage_age_range", x => x.id);
                    table.ForeignKey(
                        name: "FK_exam_supplement_dosage_age_range_exam_supplement_dosage_exam~",
                        column: x => x.exam_supplement_id,
                        principalTable: "exam_supplement_dosage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "disease",
                columns: new[] { "id", "description", "most_indicated_treatment", "name" },
                values: new object[,]
                {
                    { 1, "Influenza, commonly known as the flu, is a viral infection.", "Rest and fluids", "Influenza" },
                    { 2, "Diabetes is a chronic condition that affects how your body processes glucose.", "Insulin therapy", "Diabetes" },
                    { 3, "Asthma is a chronic lung condition that causes breathing difficulties.", "Bronchodilators and inhaled corticosteroids", "Asthma" },
                    { 4, "Hypertension, or high blood pressure, is a condition that increases the risk of heart disease and stroke.", "Lifestyle changes and antihypertensive medications", "Hypertension" },
                    { 5, "Depression is a mental health disorder characterized by persistent feelings of sadness and loss of interest.", "Therapy and antidepressant medications", "Depression" }
                });

            migrationBuilder.InsertData(
                table: "drug",
                columns: new[] { "id", "best_time_to_take", "description", "interactions", "name", "precautions" },
                values: new object[,]
                {
                    { 1, "Take with food to reduce stomach irritation.", "Aspirin is a common pain reliever and anti-inflammatory drug.", "May interact with blood-thinning medications.", "Aspirin", "Should not be taken by individuals with bleeding disorders." },
                    { 2, "Usually taken with meals to reduce gastrointestinal symptoms.", "Metformin is an oral medication used to treat type 2 diabetes.", "May interact with certain heart and kidney medications.", "Metformin", "Should not be used by individuals with kidney problems." },
                    { 3, "Use as needed during asthma attacks.", "Ventolin is a bronchodilator used to relieve asthma symptoms.", "May interact with other medications that affect heart rate.", "Ventolin", "Should not be overused; follow doctor's instructions." },
                    { 4, "Usually taken once daily, with or without food.", "Lisinopril is an angiotensin-converting enzyme (ACE) inhibitor used to treat hypertension.", "May interact with diuretics and other blood pressure medications.", "Lisinopril", "Monitor blood pressure regularly while taking this medication." },
                    { 5, "Usually taken in the morning to avoid insomnia.", "Prozac is an antidepressant medication used to treat depression and anxiety disorders.", "May interact with other medications that affect serotonin levels.", "Prozac", "Should not be abruptly discontinued; consult a doctor." }
                });

            migrationBuilder.InsertData(
                table: "exam",
                columns: new[] { "id", "description", "name", "prerequisite" },
                values: new object[,]
                {
                    { 1, "A comprehensive blood test that provides information about your health.", "Blood Panel", "Fasting for at least 8 hours before the test." },
                    { 2, "Measures your levels of different types of cholesterol in the blood.", "Cholesterol Test", "Fasting for at least 9 to 12 hours before the test." },
                    { 3, "Evaluates how well your thyroid gland is functioning.", "Thyroid Function Test", "May require fasting and avoiding certain medications before the test." },
                    { 4, "Records the electrical activity of your heart over a period of time.", "Electrocardiogram (ECG or EKG)", "No special preparations required." },
                    { 5, "Measures bone mineral density to assess the risk of osteoporosis.", "Bone Density Test", "No special preparations required." }
                });

            migrationBuilder.InsertData(
                table: "food",
                columns: new[] { "id", "best_time_to_take", "description", "interactions", "name", "precautions" },
                values: new object[,]
                {
                    { 1, null, "Salmon is a fatty fish rich in omega-3 fatty acids.", "May interact with blood-thinning medications.", "Salmon", null },
                    { 2, null, "Broccoli is a cruciferous vegetable known for its health benefits.", "No significant interactions reported.", "Broccoli", null },
                    { 3, null, "Blueberries are packed with antioxidants and nutrients.", "No significant interactions reported.", "Blueberries", null },
                    { 4, null, "Almonds are a nutritious and protein-rich nut.", "May interact with medications that lower blood sugar.", "Almonds", null },
                    { 5, null, "Spinach is a leafy green vegetable packed with vitamins and minerals.", "May interact with blood-thinning medications.", "Spinach", null }
                });

            migrationBuilder.InsertData(
                table: "lifestyle",
                columns: new[] { "id", "description", "interactions", "name", "precautions" },
                values: new object[,]
                {
                    { 1, "Engaging in regular physical activity has numerous health benefits.", "May interact with certain medical conditions and medications.", "Regular Exercise", "Consult a healthcare professional before starting a new exercise program." },
                    { 2, "Eating a balanced and nutritious diet supports overall well-being.", "May interact with certain medical conditions and medications.", "Healthy Diet", "Consult a registered dietitian for personalized dietary guidance." },
                    { 3, "Effective stress management techniques promote mental and emotional health.", "May interact with mental health conditions and medications.", "Stress Management", "Explore relaxation techniques like meditation and deep breathing." },
                    { 4, "Getting enough quality sleep is essential for physical and mental recovery.", "May interact with sleep disorders and certain medications.", "Adequate Sleep", "Prioritize a consistent sleep schedule and create a sleep-conducive environment." },
                    { 5, "Avoiding tobacco products reduces the risk of various health issues.", "Tobacco interacts negatively with almost all bodily systems.", "Tobacco-Free", "Seek professional help if you want to quit smoking or using tobacco." }
                });

            migrationBuilder.InsertData(
                table: "meal",
                columns: new[] { "id", "description", "name", "preparation_method", "total_calories" },
                values: new object[,]
                {
                    { 1, "A healthy salad with grilled chicken, mixed greens, and assorted vegetables.", "Grilled Chicken Salad", "Grill the chicken and toss it with fresh vegetables and dressing.", 350 },
                    { 2, "A colorful stir-fry with a variety of vegetables and tofu.", "Vegetable Stir-Fry", "Stir-fry the vegetables and tofu with your favorite sauce.", 300 },
                    { 3, "Baked salmon served with a side of quinoa and steamed vegetables.", "Salmon with Quinoa", "Season the salmon, bake it, and serve with cooked quinoa.", 400 },
                    { 4, "A hearty breakfast bowl with oats, berries, nuts, and yogurt.", "Oatmeal Breakfast Bowl", "Cook oats, top with berries, nuts, and yogurt.", 250 },
                    { 5, "Classic spaghetti dish with tomato marinara sauce and grated cheese.", "Spaghetti with Marinara Sauce", "Cook spaghetti and top with marinara sauce and cheese.", 350 }
                });

            migrationBuilder.InsertData(
                table: "measurement_unit",
                columns: new[] { "id", "acronym", "description", "name" },
                values: new object[,]
                {
                    { 1, "g", "A metric unit of mass.", "Gram" },
                    { 2, "ml", "A metric unit of volume.", "Milliliter" },
                    { 3, "cup", "A customary unit of volume.", "Cup" },
                    { 4, "tsp", "A customary unit of volume for small amounts of liquids.", "Teaspoon" },
                    { 5, "tbsp", "A customary unit of volume for larger amounts of liquids.", "Tablespoon" },
                    { 6, "drop", "A small amount of liquid typically dispensed by dropper.", "Drop" },
                    { 7, "tab", "A solid dose of medication typically in a flat, round shape.", "Tablet" },
                    { 8, "pill", "A solid dose of medication typically in a small, cylindrical shape.", "Pill" }
                });

            migrationBuilder.InsertData(
                table: "supplement",
                columns: new[] { "id", "best_time_to_take", "description", "interactions", "name", "precautions" },
                values: new object[,]
                {
                    { 1, "Can be taken with food to enhance absorption.", "Vitamin D is essential for strong bones and immune function.", "May interact with certain heart and kidney medications.", "Vitamin D", "Monitor vitamin D levels if taking high doses." },
                    { 2, "Can be taken with meals to reduce gastrointestinal symptoms.", "Omega-3 fatty acids are beneficial for heart and brain health.", "May interact with blood-thinning medications.", "Omega-3 Fatty Acids", "Choose high-quality supplements to avoid contaminants." },
                    { 3, "Best taken on an empty stomach.", "Probiotics promote a healthy balance of gut bacteria.", "May interact with immunosuppressive medications.", "Probiotics", "Choose strains with scientific support for desired effects." },
                    { 4, "Take on an empty stomach with vitamin C for better absorption.", "Iron is important for the production of red blood cells.", "May interact with certain antibiotics and antacids.", "Iron", "Should not be taken with calcium-rich foods or supplements." },
                    { 5, "Can be taken with meals to reduce gastrointestinal symptoms.", "Magnesium is essential for nerve and muscle function.", "May interact with certain medications for heart and bones.", "Magnesium", "Avoid excessive magnesium intake, as it can be toxic." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_country_name",
                table: "country",
                column: "name",
                unique: true);

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
                name: "IX_disease_name",
                table: "disease",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_disease_disease_disease_id",
                table: "disease_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_disease_symptom_id",
                table: "disease_disease",
                column: "symptom_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_drug_disease_id",
                table: "disease_drug",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_drug_drug_id",
                table: "disease_drug",
                column: "drug_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_drug_dosage_disease_drug_id",
                table: "disease_drug_dosage",
                column: "disease_drug_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_drug_dosage_measurement_unit_id",
                table: "disease_drug_dosage",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_drug_dosage_age_range_disease_drug_id",
                table: "disease_drug_dosage_age_range",
                column: "disease_drug_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_exam_disease_id",
                table: "disease_exam",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_exam_exam_id",
                table: "disease_exam",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_food_disease_id",
                table: "disease_food",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_food_food_id",
                table: "disease_food",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_food_dosage_disease_food_id",
                table: "disease_food_dosage",
                column: "disease_food_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_food_dosage_measurement_unit_id",
                table: "disease_food_dosage",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_food_dosage_age_range_disease_food_id",
                table: "disease_food_dosage_age_range",
                column: "disease_food_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_lifestyle_disease_id",
                table: "disease_lifestyle",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_lifestyle_lifestyle_id",
                table: "disease_lifestyle",
                column: "lifestyle_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_supplement_disease_id",
                table: "disease_supplement",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_supplement_supplement_id",
                table: "disease_supplement",
                column: "supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_supplement_dosage_disease_supplement_id",
                table: "disease_supplement_dosage",
                column: "disease_supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_supplement_dosage_measurement_unit_id",
                table: "disease_supplement_dosage",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_disease_supplement_dosage_age_range_disease_supplement_id",
                table: "disease_supplement_dosage_age_range",
                column: "disease_supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_drug_disease_disease_id",
                table: "drug_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_drug_disease_drug_id",
                table: "drug_disease",
                column: "drug_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_food_exam_id",
                table: "exam_food",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_food_food_id",
                table: "exam_food",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_lifestyle_exam_id",
                table: "exam_lifestyle",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_lifestyle_lifestyle_id",
                table: "exam_lifestyle",
                column: "lifestyle_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_result_reference_exam_id",
                table: "exam_result_reference",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_result_reference_measurement_unit_id",
                table: "exam_result_reference",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_result_reference_country_country_id",
                table: "exam_result_reference_country",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_result_reference_country_exam_result_reference_id",
                table: "exam_result_reference_country",
                column: "exam_result_reference_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_result_reference_variation_exam_result_reference_id",
                table: "exam_result_reference_variation",
                column: "exam_result_reference_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_supplement_exam_id",
                table: "exam_supplement",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_supplement_supplement_id",
                table: "exam_supplement",
                column: "supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_supplement_dosage_exam_supplement_id",
                table: "exam_supplement_dosage",
                column: "exam_supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_supplement_dosage_measurement_unit_id",
                table: "exam_supplement_dosage",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_supplement_dosage_age_range_exam_supplement_id",
                table: "exam_supplement_dosage_age_range",
                column: "exam_supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_name",
                table: "food",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_attribute_name",
                table: "food_attribute",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_disease_disease_id",
                table: "food_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_disease_food_id",
                table: "food_disease",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_healty_objective_food_id",
                table: "food_healty_objective",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_healty_objective_healty_objective_id",
                table: "food_healty_objective",
                column: "healty_objective_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_related_attribute_food_attribute_id",
                table: "food_related_attribute",
                column: "food_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_related_attribute_food_id",
                table: "food_related_attribute",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_supplement_food_id",
                table: "food_supplement",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_supplement_measurement_unit_id",
                table: "food_supplement",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_supplement_supplement_id",
                table: "food_supplement",
                column: "supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_healty_objective_name",
                table: "healty_objective",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lifestyle_disease_disease_id",
                table: "lifestyle_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_lifestyle_disease_lifestyle_id",
                table: "lifestyle_disease",
                column: "lifestyle_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_meal_food_food_id",
                table: "meal_food",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_food_meal_id",
                table: "meal_food",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_food_measurement_unit_id",
                table: "meal_food",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_period_meal_id",
                table: "meal_period",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplement_disease_disease_id",
                table: "supplement_disease",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplement_disease_supplement_id",
                table: "supplement_disease",
                column: "supplement_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_details_user_id",
                table: "user_details",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
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
                name: "dietary_option_food_attribute");

            migrationBuilder.DropTable(
                name: "disease_disease");

            migrationBuilder.DropTable(
                name: "disease_drug_dosage_age_range");

            migrationBuilder.DropTable(
                name: "disease_exam");

            migrationBuilder.DropTable(
                name: "disease_food_dosage_age_range");

            migrationBuilder.DropTable(
                name: "disease_lifestyle");

            migrationBuilder.DropTable(
                name: "disease_supplement_dosage_age_range");

            migrationBuilder.DropTable(
                name: "drug_disease");

            migrationBuilder.DropTable(
                name: "exam_food");

            migrationBuilder.DropTable(
                name: "exam_lifestyle");

            migrationBuilder.DropTable(
                name: "exam_result_reference_country");

            migrationBuilder.DropTable(
                name: "exam_result_reference_variation");

            migrationBuilder.DropTable(
                name: "exam_supplement_dosage_age_range");

            migrationBuilder.DropTable(
                name: "food_disease");

            migrationBuilder.DropTable(
                name: "food_healty_objective");

            migrationBuilder.DropTable(
                name: "food_related_attribute");

            migrationBuilder.DropTable(
                name: "food_supplement");

            migrationBuilder.DropTable(
                name: "lifestyle_disease");

            migrationBuilder.DropTable(
                name: "meal_country");

            migrationBuilder.DropTable(
                name: "meal_dietary_option");

            migrationBuilder.DropTable(
                name: "meal_food");

            migrationBuilder.DropTable(
                name: "meal_period");

            migrationBuilder.DropTable(
                name: "supplement_disease");

            migrationBuilder.DropTable(
                name: "user_details");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "disease_drug_dosage");

            migrationBuilder.DropTable(
                name: "disease_food_dosage");

            migrationBuilder.DropTable(
                name: "disease_supplement_dosage");

            migrationBuilder.DropTable(
                name: "exam_result_reference");

            migrationBuilder.DropTable(
                name: "exam_supplement_dosage");

            migrationBuilder.DropTable(
                name: "healty_objective");

            migrationBuilder.DropTable(
                name: "food_attribute");

            migrationBuilder.DropTable(
                name: "lifestyle");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "dietary_option");

            migrationBuilder.DropTable(
                name: "meal");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "disease_drug");

            migrationBuilder.DropTable(
                name: "disease_food");

            migrationBuilder.DropTable(
                name: "disease_supplement");

            migrationBuilder.DropTable(
                name: "exam_supplement");

            migrationBuilder.DropTable(
                name: "measurement_unit");

            migrationBuilder.DropTable(
                name: "drug");

            migrationBuilder.DropTable(
                name: "food");

            migrationBuilder.DropTable(
                name: "disease");

            migrationBuilder.DropTable(
                name: "exam");

            migrationBuilder.DropTable(
                name: "supplement");
        }
    }
}
