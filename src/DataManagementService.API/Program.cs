using System.Text;
using System.Text.Json.Serialization;
using DataManagementService.Application;
using DataManagementService.Application.Interfaces;
using DataManagementService.Application.Interfaces.UserInterfaces;
using DataManagementService.Application.UserServices;
using DataManagementService.Domain.Identity;
using DataManagementService.Persistence;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using DataManagementService.Persistence.Interfaces.Idenity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration["MySqlConnection:MysqlConnectionString"];
builder.Services.AddDbContext<DataManagementServiceContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(10, 4, 28))));

builder.Services.AddIdentityCore<User>
    (options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
        })
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<User>>()
    .AddRoleValidator<RoleValidator<IdentityRole>>()
    .AddEntityFrameworkStores<DataManagementServiceContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options => 
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    );

builder.Services.AddControllers()
    // .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IDietaryOptionService, DietaryOptionService>();
builder.Services.AddScoped<IDietaryOptionFoodAttributeService, DietaryOptionFoodAttributeService>();

builder.Services.AddScoped<IDiseaseService, DiseaseService>();
builder.Services.AddScoped<IDiseaseDrugService, DiseaseDrugService>();
builder.Services.AddScoped<IDiseaseDrugDosageService, DiseaseDrugDosageService>();
builder.Services.AddScoped<IDiseaseDrugDosageAgeRangeService, DiseaseDrugDosageAgeRangeService>();
builder.Services.AddScoped<IDiseaseSupplementService, DiseaseSupplementService>();
builder.Services.AddScoped<IDiseaseSupplementDosageService, DiseaseSupplementDosageService>();
builder.Services.AddScoped<IDiseaseSupplementDosageAgeRangeService, DiseaseSupplementDosageAgeRangeService>();
builder.Services.AddScoped<IDiseaseFoodService, DiseaseFoodService>();
builder.Services.AddScoped<IDiseaseFoodDosageService, DiseaseFoodDosageService>();
builder.Services.AddScoped<IDiseaseFoodDosageAgeRangeService, DiseaseFoodDosageAgeRangeService>();
builder.Services.AddScoped<IDiseaseLifestyleService, DiseaseLifestyleService>();
builder.Services.AddScoped<IDiseaseDiseaseService, DiseaseDiseaseService>();
builder.Services.AddScoped<IDiseaseExamService, DiseaseExamService>();

builder.Services.AddScoped<IDrugService, DrugService>();
builder.Services.AddScoped<IDrugDiseaseService, DrugDiseaseService>();

builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IExamResultReferenceService, ExamResultReferenceService>();
builder.Services.AddScoped<IExamResultReferenceCountryService, ExamResultReferenceCountryService>();
builder.Services.AddScoped<IExamResultReferenceVariationService, ExamResultReferenceVariationService>();
builder.Services.AddScoped<IExamSupplementService, ExamSupplementService>();
builder.Services.AddScoped<IExamSupplementDosageService, ExamSupplementDosageService>();
builder.Services.AddScoped<IExamSupplementDosageAgeRangeService, ExamSupplementDosageAgeRangeService>();
builder.Services.AddScoped<IExamFoodService, ExamFoodService>();
builder.Services.AddScoped<IExamLifestyleService, ExamLifestyleService>();

builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodDiseaseService, FoodDiseaseService>();
builder.Services.AddScoped<IFoodHealtyObjectiveService, FoodHealtyObjectiveService>();
builder.Services.AddScoped<IFoodSupplementService, FoodSupplementService>();
builder.Services.AddScoped<IFoodRelatedAttributeService, FoodRelatedAttributeService>();

builder.Services.AddScoped<IFoodAttributeService, FoodAttributeService>();

builder.Services.AddScoped<IHealtyObjectiveService, HealtyObjectiveService>();

builder.Services.AddScoped<ILifestyleService, LifestyleService>();
builder.Services.AddScoped<ILifestyleDiseaseService, LifestyleDiseaseService>();

builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IMealFoodService, MealFoodService>();
builder.Services.AddScoped<IMealPeriodService, MealPeriodService>();
builder.Services.AddScoped<IMealCountryService, MealCountryService>();
builder.Services.AddScoped<IMealDietaryOptionService, MealDietaryOptionService>();

builder.Services.AddScoped<IMeasurementUnitService, MeasurementUnitService>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddScoped<ISupplementService, SupplementService>();
builder.Services.AddScoped<ISupplementDiseaseService, SupplementDiseaseService>();

builder.Services.AddScoped<IUserPersistence, UserPersistence>();

builder.Services.AddScoped<IGeneralPersistence, GeneralPersistence>();

builder.Services.AddScoped<IDietaryOptionPersistence, DietaryOptionPersistence>();
builder.Services.AddScoped<IDietaryOptionFoodAttributePersistence, DietaryOptionFoodAttributePersistence>();

builder.Services.AddScoped<IDiseasePersistence, DiseasePersistence>();
builder.Services.AddScoped<IDiseaseDrugPersistence, DiseaseDrugPersistence>();
builder.Services.AddScoped<IDiseaseDrugDosagePersistence, DiseaseDrugDosagePersistence>();
builder.Services.AddScoped<IDiseaseDrugDosageAgeRangePersistence, DiseaseDrugDosageAgeRangePersistence>();
builder.Services.AddScoped<IDiseaseSupplementPersistence, DiseaseSupplementPersistence>();
builder.Services.AddScoped<IDiseaseSupplementDosagePersistence, DiseaseSupplementDosagePersistence>();
builder.Services.AddScoped<IDiseaseSupplementDosageAgeRangePersistence, DiseaseSupplementDosageAgeRangePersistence>();
builder.Services.AddScoped<IDiseaseFoodPersistence, DiseaseFoodPersistence>();
builder.Services.AddScoped<IDiseaseFoodDosagePersistence, DiseaseFoodDosagePersistence>();
builder.Services.AddScoped<IDiseaseFoodDosageAgeRangePersistence, DiseaseFoodDosageAgeRangePersistence>();
builder.Services.AddScoped<IDiseaseLifestylePersistence, DiseaseLifestylePersistence>();
builder.Services.AddScoped<IDiseaseDiseasePersistence, DiseaseDiseasePersistence>();
builder.Services.AddScoped<IDiseaseExamPersistence, DiseaseExamPersistence>();

builder.Services.AddScoped<IDrugPersistence, DrugPersistence>();
builder.Services.AddScoped<IDrugDiseasePersistence, DrugDiseasePersistence>();

builder.Services.AddScoped<IExamPersistence, ExamPersistence>();
builder.Services.AddScoped<IExamResultReferencePersistence, ExamResultReferencePersistence>();
builder.Services.AddScoped<IExamResultReferenceCountryPersistence, ExamResultReferenceCountryPersistence>();
builder.Services.AddScoped<IExamResultReferenceVariationPersistence, ExamResultReferenceVariationPersistence>();
builder.Services.AddScoped<IExamSupplementPersistence, ExamSupplementPersistence>();
builder.Services.AddScoped<IExamSupplementDosagePersistence, ExamSupplementDosagePersistence>();
builder.Services.AddScoped<IExamSupplementDosageAgeRangePersistence, ExamSupplementDosageAgeRangePersistence>();
builder.Services.AddScoped<IExamFoodPersistence, ExamFoodPersistence>();
builder.Services.AddScoped<IExamLifestylePersistence, ExamLifestylePersistence>();

builder.Services.AddScoped<IFoodPersistence, FoodPersistence>();
builder.Services.AddScoped<IFoodDiseasePersistence, FoodDiseasePersistence>();
builder.Services.AddScoped<IFoodHealtyObjectivePersistence, FoodHealtyObjectivePersistence>();
builder.Services.AddScoped<IFoodSupplementPersistence, FoodSupplementPersistence>();
builder.Services.AddScoped<IFoodRelatedAttributePersistence, FoodRelatedAttributePersistence>();

builder.Services.AddScoped<IFoodAttributePersistence, FoodAttributePersistence>();

builder.Services.AddScoped<IHealtyObjectivePersistence, HealtyObjectivePersistence>();

builder.Services.AddScoped<ILifestylePersistence, LifestylePersistence>();
builder.Services.AddScoped<ILifestyleDiseasePersistence, LifestyleDiseasePersistence>();

builder.Services.AddScoped<IMealPersistence, MealPersistence>();
builder.Services.AddScoped<IMealFoodPersistence, MealFoodPersistence>();
builder.Services.AddScoped<IMealPeriodPersistence, MealPeriodPersistence>();
builder.Services.AddScoped<IMealCountryPersistence, MealCountryPersistence>();
builder.Services.AddScoped<IMealDietaryOptionPersistence, MealDietaryOptionPersistence>();

builder.Services.AddScoped<IMeasurementUnitPersistence, MeasurementUnitPersistence>();
builder.Services.AddScoped<ICountryPersistence, CountryPersistence>();

builder.Services.AddScoped<ISupplementPersistence, SupplementPersistence>();
builder.Services.AddScoped<ISupplementDiseasePersistence, SupplementDiseasePersistence>();

builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "DataManagementServer.API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
        {
            Description = @"<b>JWT Authorization header using Bearer.</b>
                <p>Enter with 'Bearer' [space] then put your token.</p>
                <p>Example: 'Bearer 123456abcdef'.</p><br>",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
        
                },
                new List<string>()
            }
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();
