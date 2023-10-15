using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence.Contexts
{
    public class DataManagementServiceContext : IdentityDbContext<User, IdentityRole, string, IdentityUserClaim<string>,
     IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataManagementServiceContext(DbContextOptions<DataManagementServiceContext> options) : base(options) { }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DietaryOption> DietaryOptions { get; set; }
        public DbSet<DietaryOptionFoodAttribute> DietaryOptionFoodAttributes { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<DiseaseDrug> DiseaseDrugs { get; set; }
        public DbSet<DiseaseDrugDosage> DiseaseDrugDosages { get; set; }
        public DbSet<DiseaseDrugDosageAgeRange> DiseaseDrugDosageAgeRanges { get; set; }
        public DbSet<DiseaseSupplement> DiseaseSupplements { get; set; }
        public DbSet<DiseaseSupplementDosage> DiseaseSupplementDosages { get; set; }
        public DbSet<DiseaseSupplementDosageAgeRange> DiseaseSupplementDosageAgeRanges { get; set; }
        public DbSet<DiseaseFood> DiseaseFoods { get; set; }
        public DbSet<DiseaseFoodDosage> DiseaseFoodDosages { get; set; }
        public DbSet<DiseaseFoodDosageAgeRange> DiseaseFoodDosageAgeRanges { get; set; }
        public DbSet<DiseaseLifestyle> DiseaseLifestyles { get; set; }
        public DbSet<DiseaseDisease> DiseaseDiseases { get; set; }
        public DbSet<DiseaseExam> DiseaseExams { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugDisease> DrugDiseases { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResultReference> ExamResultReferences { get; set; }
        public DbSet<ExamResultReferenceVariation> ExamResultReferenceVariations { get; set; }
        public DbSet<ExamResultReferenceCountry> ExamResultReferenceCountries { get; set; }
        public DbSet<ExamFood> ExamFoods { get; set; }
        public DbSet<ExamLifestyle> ExamLifestyles { get; set; }
        public DbSet<ExamSupplement> ExamSupplements { get; set; }
        public DbSet<ExamSupplementDosage> ExamSupplementDosages { get; set; }
        public DbSet<ExamSupplementDosageAgeRange> ExamSupplementDosageAgeRanges { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodDisease> FoodDiseases { get; set; }
        public DbSet<FoodRelatedAttribute> FoodRelatedAttributes { get; set; }
        public DbSet<FoodHealtyObjective> FoodHealtyObjectives { get; set; }
        public DbSet<FoodSupplement> FoodSupplements { get; set; }
        public DbSet<FoodAttribute> FoodAttributes { get; set; }
        public DbSet<HealtyObjective> HealtyObjectives { get; set; }
        public DbSet<Lifestyle> Lifestyles { get; set; }
        public DbSet<LifestyleDisease> LifestyleDiseases { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealCountry> MealCountries { get; set; }
        public DbSet<MealFood> MealFoods { get; set; }
        public DbSet<MealPeriod> MealPeriods { get; set; }
        public DbSet<MealDietaryOption> MealDietaryOptions { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<SupplementDisease> SupplementDiseases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /***************************************
                TABLES CONFIGURATIONS
            ****************************************/

            //UserDetails
            modelBuilder.Entity<UserDetails>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.User)
                .WithOne(u => u.Details)
                .HasForeignKey<UserDetails>(ud => ud.UserId);

            //Country
            modelBuilder.Entity<Country>()
                .Property(d => d.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<Country>()
                .HasIndex(d => d.Name)
                .IsUnique();

            //DietaryOption
            modelBuilder.Entity<DietaryOption>()
                .Property(d => d.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DietaryOption>()
                .HasIndex(d => d.Name)
                .IsUnique();

            //DietaryOptionFoodAttribute
            modelBuilder.Entity<DietaryOptionFoodAttribute>()
                .Property(dofa => dofa.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DietaryOptionFoodAttribute>()
                .HasOne(dofa => dofa.DietaryOption)
                .WithMany(diopt => diopt.Incompatibilities)
                .HasForeignKey(dofa => dofa.DietaryOptionId);
            modelBuilder.Entity<DietaryOptionFoodAttribute>()
                .HasOne(dofa => dofa.FoodAttribute)
                .WithMany(fa => fa.Incompatibilities)
                .HasForeignKey(dofa => dofa.FoodAttributeId);

            //Disease
            modelBuilder.Entity<Disease>()
                .Property(d => d.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<Disease>()
                .HasIndex(d => d.Name)
                .IsUnique();

            //DiseaseDrug
            modelBuilder.Entity<DiseaseDrug>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseDrug>()
                .HasOne(dd => dd.Disease)
                .WithMany(disease => disease.TreatmentDrugs)
                .HasForeignKey(dd => dd.DiseaseId);
            modelBuilder.Entity<DiseaseDrug>()
                .HasOne(dd => dd.Drug)
                .WithMany(drug => drug.TreatableDiseases)
                .HasForeignKey(dd => dd.DrugId);

            //DiseaseDrugDosage
            modelBuilder.Entity<DiseaseDrugDosage>()
                .Property(ddd => ddd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseDrugDosage>()
                .HasOne(ddd => ddd.DiseaseDrug)
                .WithMany(dd => dd.Dosages)
                .HasForeignKey(ddd => ddd.DiseaseDrugId);
            modelBuilder.Entity<DiseaseDrugDosage>()
                .HasOne(ddd => ddd.MeasurementUnit)
                .WithMany()
                .HasForeignKey(ddd => ddd.MeasurementUnitId);

            //DiseaseDrugDosageAgeRange
            modelBuilder.Entity<DiseaseDrugDosageAgeRange>()
                .Property(dddar => dddar.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseDrugDosageAgeRange>()
                .Property(dddar => dddar.AgeTimeUnit)
                .HasComment("1=days; 2=months; 3=years");
            modelBuilder.Entity<DiseaseDrugDosageAgeRange>()
                .Property(dddar => dddar.MaxUsagePeriod)
                .HasComment("in days");
            modelBuilder.Entity<DiseaseDrugDosageAgeRange>()
                .HasOne(dddar => dddar.DiseaseDrugDosage)
                .WithMany(ddd => ddd.AgeRanges)
                .HasForeignKey(dddar => dddar.DiseaseDrugDosageId);

            //DiseaseSupplement
            modelBuilder.Entity<DiseaseSupplement>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseSupplement>()
                .HasOne(dd => dd.Disease)
                .WithMany(disease => disease.TreatmentSupplements)
                .HasForeignKey(dd => dd.DiseaseId);
            modelBuilder.Entity<DiseaseSupplement>()
                .HasOne(dd => dd.Supplement)
                .WithMany(supplement => supplement.TreatableDiseases)
                .HasForeignKey(dd => dd.SupplementId);

            //DiseaseSupplementDosage
            modelBuilder.Entity<DiseaseSupplementDosage>()
                .Property(ddd => ddd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseSupplementDosage>()
                .HasOne(ddd => ddd.DiseaseSupplement)
                .WithMany(dd => dd.Dosages)
                .HasForeignKey(ddd => ddd.DiseaseSupplementId);
            modelBuilder.Entity<DiseaseSupplementDosage>()
                .HasOne(ddd => ddd.MeasurementUnit)
                .WithMany()
                .HasForeignKey(ddd => ddd.MeasurementUnitId);

            //DiseaseSupplementDosageAgeRange
            modelBuilder.Entity<DiseaseSupplementDosageAgeRange>()
                .Property(dddar => dddar.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseSupplementDosageAgeRange>()
                .Property(dddar => dddar.AgeTimeUnit)
                .HasComment("1=days; 2=months; 3=years");
            modelBuilder.Entity<DiseaseSupplementDosageAgeRange>()
                .Property(dddar => dddar.MaxUsagePeriod)
                .HasComment("in days");
            modelBuilder.Entity<DiseaseSupplementDosageAgeRange>()
                .HasOne(dddar => dddar.DiseaseSupplementDosage)
                .WithMany(ddd => ddd.AgeRanges)
                .HasForeignKey(dddar => dddar.DiseaseSupplementDosageId);

            //DiseaseFood
            modelBuilder.Entity<DiseaseFood>()
                .Property(df => df.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseFood>()
                .HasOne(df => df.Disease)
                .WithMany(disease => disease.TreatmentFoods)
                .HasForeignKey(df => df.DiseaseId);
            modelBuilder.Entity<DiseaseFood>()
                .HasOne(df => df.Food)
                .WithMany(food => food.TreatableDiseases)
                .HasForeignKey(df => df.FoodId);

            //DiseaseFoodDosage
            modelBuilder.Entity<DiseaseFoodDosage>()
                .Property(dfd => dfd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseFoodDosage>()
                .HasOne(dfd => dfd.DiseaseFood)
                .WithMany(dd => dd.Dosages)
                .HasForeignKey(dfd => dfd.DiseaseFoodId);
            modelBuilder.Entity<DiseaseFoodDosage>()
                .HasOne(dfd => dfd.MeasurementUnit)
                .WithMany()
                .HasForeignKey(dfd => dfd.MeasurementUnitId);

            //DiseaseFoodDosageAgeRange
            modelBuilder.Entity<DiseaseFoodDosageAgeRange>()
                .Property(dfdar => dfdar.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseFoodDosageAgeRange>()
                .Property(dfdar => dfdar.AgeTimeUnit)
                .HasComment("1=days; 2=months; 3=years");
            modelBuilder.Entity<DiseaseFoodDosageAgeRange>()
                .Property(dfdar => dfdar.MaxUsagePeriod)
                .HasComment("in days");
            modelBuilder.Entity<DiseaseFoodDosageAgeRange>()
                .HasOne(dfdar => dfdar.DiseaseFoodDosage)
                .WithMany(dfd => dfd.AgeRanges)
                .HasForeignKey(dfdar => dfdar.DiseaseFoodDosageId);

            //DiseaseLifestyle
            modelBuilder.Entity<DiseaseLifestyle>()
                .Property(dl => dl.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseLifestyle>()
                .HasOne(dl => dl.Disease)
                .WithMany(d => d.TreatmentLifestyles)
                .HasForeignKey(dl => dl.DiseaseId);
            modelBuilder.Entity<DiseaseLifestyle>()
                .HasOne(dl => dl.Lifestyle)
                .WithMany(l => l.RelatedDiseases)
                .HasForeignKey(dl => dl.LifestyleId);

            //DiseaseDisease
            modelBuilder.Entity<DiseaseDisease>()
                .HasKey(dd => dd.Id);
            modelBuilder.Entity<DiseaseDisease>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseDisease>()
                .HasOne(dd => dd.Disease)
                .WithMany(disease => disease.DiagnoseSymptoms)
                .HasForeignKey(dd => dd.DiseaseId);
            modelBuilder.Entity<DiseaseDisease>()
                .HasOne(dd => dd.Symptom)
                .WithMany(disease => disease.SymptomOfDiseases)
                .HasForeignKey(dd => dd.SymptomId);

            //DiseaseExam
            modelBuilder.Entity<DiseaseExam>()
                .Property(de => de.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DiseaseExam>()
                .HasOne(de => de.Disease)
                .WithMany(d => d.DiagnoseExams)
                .HasForeignKey(de => de.DiseaseId);
            modelBuilder.Entity<DiseaseExam>()
                .HasOne(de => de.Exam)
                .WithMany(l => l.RelatedDiseases)
                .HasForeignKey(de => de.ExamId);

            //Drug
            modelBuilder.Entity<Drug>()
                .Property(drug => drug.Id)
                .UseMySqlIdentityColumn();

            //DrugDisease
            modelBuilder.Entity<DrugDisease>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<DrugDisease>()
                .HasOne(dd => dd.Drug)
                .WithMany(drug => drug.SideEffects)
                .HasForeignKey(dd => dd.DrugId);
            modelBuilder.Entity<DrugDisease>()
                .HasOne(dd => dd.Disease)
                .WithMany(d => d.SideEffectOfDrugs)
                .HasForeignKey(dd => dd.DiseaseId);

            //Exam
            modelBuilder.Entity<Exam>()
                .Property(exam => exam.Id)
                .UseMySqlIdentityColumn();

            //ExamSupplement
            modelBuilder.Entity<ExamSupplement>()
                .Property(es => es.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamSupplement>()
                .HasOne(es => es.Exam)
                .WithMany(e => e.RelatedSupplements)
                .HasForeignKey(es => es.ExamId);
            modelBuilder.Entity<ExamSupplement>()
                .HasOne(es => es.Supplement)
                .WithMany(s => s.RelatedExams)
                .HasForeignKey(es => es.SupplementId);

            //ExamSupplementDosage
            modelBuilder.Entity<ExamSupplementDosage>()
                .Property(ddd => ddd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamSupplementDosage>()
                .HasOne(ddd => ddd.ExamSupplement)
                .WithMany(dd => dd.Dosages)
                .HasForeignKey(ddd => ddd.ExamSupplementId);
            modelBuilder.Entity<ExamSupplementDosage>()
                .HasOne(ddd => ddd.MeasurementUnit)
                .WithMany()
                .HasForeignKey(ddd => ddd.MeasurementUnitId);

            //ExamSupplementDosageAgeRange
            modelBuilder.Entity<ExamSupplementDosageAgeRange>()
                .Property(dddar => dddar.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamSupplementDosageAgeRange>()
                .Property(dddar => dddar.AgeTimeUnit)
                .HasComment("1=days; 2=months; 3=years");
            modelBuilder.Entity<ExamSupplementDosageAgeRange>()
                .Property(dddar => dddar.MaxUsagePeriod)
                .HasComment("in days");
            modelBuilder.Entity<ExamSupplementDosageAgeRange>()
                .HasOne(dddar => dddar.ExamSupplementDosage)
                .WithMany(ddd => ddd.AgeRanges)
                .HasForeignKey(dddar => dddar.ExamSupplementDosageId);

            //ExamFood
            modelBuilder.Entity<ExamFood>()
                .Property(ef => ef.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamFood>()
                .HasOne(ef => ef.Exam)
                .WithMany(e => e.RelatedFoods)
                .HasForeignKey(ef => ef.ExamId);
            modelBuilder.Entity<ExamFood>()
                .HasOne(ef => ef.Food)
                .WithMany(f => f.RelatedExams)
                .HasForeignKey(ef => ef.FoodId);

            //ExamLifestyle
            modelBuilder.Entity<ExamLifestyle>()
                .Property(el => el.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamLifestyle>()
                .HasOne(el => el.Exam)
                .WithMany(e => e.RelatedLifestyles)
                .HasForeignKey(el => el.ExamId);
            modelBuilder.Entity<ExamLifestyle>()
                .HasOne(el => el.Lifestyle)
                .WithMany(l => l.RelatedExams)
                .HasForeignKey(el => el.LifestyleId);

            //ExamResultReference
            modelBuilder.Entity<ExamResultReference>()
                .Property(err => err.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamResultReference>()
                .HasOne(err => err.Exam)
                .WithMany(e => e.ExamResultReferences)
                .HasForeignKey(err => err.ExamId);
            modelBuilder.Entity<ExamResultReference>()
                .HasOne(err => err.MeasurementUnit)
                .WithMany()
                .HasForeignKey(err => err.MeasurementUnitId);

            //ExamResultReferenceCountry
            modelBuilder.Entity<ExamResultReferenceCountry>()
                .Property(errc => errc.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamResultReferenceCountry>()
                .HasOne(errc => errc.ExamResultReference)
                .WithMany(err => err.Countries)
                .HasForeignKey(errc => errc.ExamResultReferenceId);
            modelBuilder.Entity<ExamResultReferenceCountry>()
                .HasOne(errc => errc.Country)
                .WithMany()
                .HasForeignKey(errc => errc.CountryId);

            //ExamResultReferenceVariation
            modelBuilder.Entity<ExamResultReferenceVariation>()
                .Property(v => v.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<ExamResultReferenceVariation>()
                .HasOne(v => v.ExamResultReference)
                .WithMany(err => err.Variations)
                .HasForeignKey(v => v.ExamResultReferenceId);


            //Food
            modelBuilder.Entity<Food>()
                .Property(d => d.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<Food>()
                .HasIndex(d => d.Name)
                .IsUnique();

            //FoodDisease
            modelBuilder.Entity<FoodDisease>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<FoodDisease>()
                .HasOne(dd => dd.Food)
                .WithMany(food => food.SideEffects)
                .HasForeignKey(dd => dd.FoodId);
            modelBuilder.Entity<FoodDisease>()
                .HasOne(dd => dd.Disease)
                .WithMany(d => d.SideEffectOfFoods)
                .HasForeignKey(dd => dd.DiseaseId);

            //FoodHealtyObjective
            modelBuilder.Entity<FoodHealtyObjective>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<FoodHealtyObjective>()
                .HasOne(dd => dd.Food)
                .WithMany(food => food.RelatedHealtyObjectives)
                .HasForeignKey(dd => dd.FoodId);
            modelBuilder.Entity<FoodHealtyObjective>()
                .HasOne(dd => dd.HealtyObjective)
                .WithMany(d => d.RelatedFoods)
                .HasForeignKey(dd => dd.HealtyObjectiveId);

            //FoodSupplement
            modelBuilder.Entity<FoodSupplement>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<FoodSupplement>()
                .HasOne(dd => dd.Food)
                .WithMany(food => food.Nutrients)
                .HasForeignKey(dd => dd.FoodId);
            modelBuilder.Entity<FoodSupplement>()
                .HasOne(dd => dd.Supplement)
                .WithMany(supplement => supplement.FoundIn)
                .HasForeignKey(dd => dd.SupplementId);

            //FoodRelatedAttribute
            modelBuilder.Entity<FoodRelatedAttribute>()
                .Property(fra => fra.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<FoodRelatedAttribute>()
                .HasOne(fra => fra.Food)
                .WithMany(food => food.Attributes)
                .HasForeignKey(fra => fra.FoodId);
            modelBuilder.Entity<FoodRelatedAttribute>()
                .HasOne(fra => fra.FoodAttribute)
                .WithMany(fa => fa.RelatedFoods)
                .HasForeignKey(fra => fra.FoodAttributeId);

            //FoodAttribute => Caution! this is not a food relationship, there is another entitie
            modelBuilder.Entity<FoodAttribute>()
                .Property(d => d.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<FoodAttribute>()
                .HasIndex(d => d.Name)
                .IsUnique();

            //HealtyObjective
            modelBuilder.Entity<HealtyObjective>()
                .Property(d => d.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<HealtyObjective>()
                .HasIndex(d => d.Name)
                .IsUnique();

            //Supplement
            modelBuilder.Entity<Supplement>()
                .Property(supplement => supplement.Id)
                .UseMySqlIdentityColumn();

            //SupplementDisease
            modelBuilder.Entity<SupplementDisease>()
                .Property(sd => sd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<SupplementDisease>()
                .HasOne(sd => sd.Supplement)
                .WithMany(supplement => supplement.SideEffects)
                .HasForeignKey(sd => sd.SupplementId);
            modelBuilder.Entity<SupplementDisease>()
                .HasOne(sd => sd.Disease)
                .WithMany(d => d.SideEffectOfSupplements)
                .HasForeignKey(sd => sd.DiseaseId);

            //Lifestyle
            modelBuilder.Entity<Lifestyle>()
                .Property(lifestyle => lifestyle.Id)
                .UseMySqlIdentityColumn();

            //LifestyleDisease
            modelBuilder.Entity<LifestyleDisease>()
                .Property(dd => dd.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<LifestyleDisease>()
                .HasOne(dd => dd.Lifestyle)
                .WithMany(lifestyle => lifestyle.SideEffects)
                .HasForeignKey(dd => dd.LifestyleId);
            modelBuilder.Entity<LifestyleDisease>()
                .HasOne(dd => dd.Disease)
                .WithMany(d => d.SideEffectOfLifestyles)
                .HasForeignKey(dd => dd.DiseaseId);

            //Meal
            modelBuilder.Entity<Meal>()
                .Property(meal => meal.Id)
                .UseMySqlIdentityColumn();

            //MealFood
            modelBuilder.Entity<MealFood>()
                .Property(mf => mf.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<MealFood>()
                .HasOne(mf => mf.Meal)
                .WithMany(meal => meal.Ingredients)
                .HasForeignKey(mf => mf.MealId);
            modelBuilder.Entity<MealFood>()
                .HasOne(mf => mf.Food)
                .WithMany(f => f.FoundIn)
                .HasForeignKey(mf => mf.FoodId);

            //MealPeriod
            modelBuilder.Entity<MealPeriod>()
                .Property(mp => mp.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<MealPeriod>()
                .HasOne(mp => mp.Meal)
                .WithMany(meal => meal.MealPeriods)
                .HasForeignKey(mp => mp.MealId);

            //MealCountry
            modelBuilder.Entity<MealCountry>()
                .Property(mc => mc.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<MealCountry>()
                .HasOne(mc => mc.Meal)
                .WithMany(meal => meal.InternationalCuisines)
                .HasForeignKey(mc => mc.MealId);
            modelBuilder.Entity<MealCountry>()
                .HasOne(mc => mc.Country)
                .WithMany(c => c.MealCountries)
                .HasForeignKey(mc => mc.CountryId);

            //MealDietaryOption
            modelBuilder.Entity<MealDietaryOption>()
                .Property(mdo => mdo.Id)
                .UseMySqlIdentityColumn();
            modelBuilder.Entity<MealDietaryOption>()
                .HasOne(mdo => mdo.Meal)
                .WithMany(meal => meal.MealDietaryOptions)
                .HasForeignKey(mdo => mdo.MealId);
            modelBuilder.Entity<MealDietaryOption>()
                .HasOne(mdo => mdo.DietaryOption)
                .WithMany(diopt => diopt.MealDietaryOptions)
                .HasForeignKey(mdo => mdo.DietaryOptionId);

            /***************************************
                MOCKS
            ****************************************/

            // //DmsUser
            // modelBuilder.Entity<DmsUser>().HasData(
            //     new DmsUser { Id = 1, Name = "John Doe", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
            //     new DmsUser { Id = 2, Name = "Jane Smith", Description = "Nullam nec odio ac justo auctor auctor in nec elit." },
            //     new DmsUser { Id = 3, Name = "Alice Johnson", Description = "Phasellus malesuada justo at odio pretium, in efficitur elit efficitur." },
            //     new DmsUser { Id = 4, Name = "Bob Brown", Description = "Fusce id justo vel erat congue porttitor." },
            //     new DmsUser { Id = 5, Name = "Emily Davis", Description = "Sed sed urna at libero hendrerit blandit." }
            // );

            //Disease
            modelBuilder.Entity<Disease>().HasData(
                new Disease { Id = 1, Name = "Influenza", Description = "Influenza, commonly known as the flu, is a viral infection.", MostIndicatedTreatment = "Rest and fluids" },
                new Disease { Id = 2, Name = "Diabetes", Description = "Diabetes is a chronic condition that affects how your body processes glucose.", MostIndicatedTreatment = "Insulin therapy" },
                new Disease { Id = 3, Name = "Asthma", Description = "Asthma is a chronic lung condition that causes breathing difficulties.", MostIndicatedTreatment = "Bronchodilators and inhaled corticosteroids" },
                new Disease { Id = 4, Name = "Hypertension", Description = "Hypertension, or high blood pressure, is a condition that increases the risk of heart disease and stroke.", MostIndicatedTreatment = "Lifestyle changes and antihypertensive medications" },
                new Disease { Id = 5, Name = "Depression", Description = "Depression is a mental health disorder characterized by persistent feelings of sadness and loss of interest.", MostIndicatedTreatment = "Therapy and antidepressant medications" }
            );

            //Drug
            modelBuilder.Entity<Drug>().HasData(
                new Drug
                {
                    Id = 1,
                    Name = "Aspirin",
                    Description = "Aspirin is a common pain reliever and anti-inflammatory drug.",
                    Interactions = "May interact with blood-thinning medications.",
                    Precautions = "Should not be taken by individuals with bleeding disorders.",
                    BestTimeToTake = "Take with food to reduce stomach irritation."
                },
                new Drug
                {
                    Id = 2,
                    Name = "Metformin",
                    Description = "Metformin is an oral medication used to treat type 2 diabetes.",
                    Interactions = "May interact with certain heart and kidney medications.",
                    Precautions = "Should not be used by individuals with kidney problems.",
                    BestTimeToTake = "Usually taken with meals to reduce gastrointestinal symptoms."
                },
                new Drug
                {
                    Id = 3,
                    Name = "Ventolin",
                    Description = "Ventolin is a bronchodilator used to relieve asthma symptoms.",
                    Interactions = "May interact with other medications that affect heart rate.",
                    Precautions = "Should not be overused; follow doctor's instructions.",
                    BestTimeToTake = "Use as needed during asthma attacks."
                },
                new Drug
                {
                    Id = 4,
                    Name = "Lisinopril",
                    Description = "Lisinopril is an angiotensin-converting enzyme (ACE) inhibitor used to treat hypertension.",
                    Interactions = "May interact with diuretics and other blood pressure medications.",
                    Precautions = "Monitor blood pressure regularly while taking this medication.",
                    BestTimeToTake = "Usually taken once daily, with or without food."
                },
                new Drug
                {
                    Id = 5,
                    Name = "Prozac",
                    Description = "Prozac is an antidepressant medication used to treat depression and anxiety disorders.",
                    Interactions = "May interact with other medications that affect serotonin levels.",
                    Precautions = "Should not be abruptly discontinued; consult a doctor.",
                    BestTimeToTake = "Usually taken in the morning to avoid insomnia."
                }
            );

            //Supplement
            modelBuilder.Entity<Supplement>().HasData(
                new Supplement
                {
                    Id = 1,
                    Name = "Vitamin D",
                    Description = "Vitamin D is essential for strong bones and immune function.",
                    Interactions = "May interact with certain heart and kidney medications.",
                    Precautions = "Monitor vitamin D levels if taking high doses.",
                    BestTimeToTake = "Can be taken with food to enhance absorption."
                },
                new Supplement
                {
                    Id = 2,
                    Name = "Omega-3 Fatty Acids",
                    Description = "Omega-3 fatty acids are beneficial for heart and brain health.",
                    Interactions = "May interact with blood-thinning medications.",
                    Precautions = "Choose high-quality supplements to avoid contaminants.",
                    BestTimeToTake = "Can be taken with meals to reduce gastrointestinal symptoms."
                },
                new Supplement
                {
                    Id = 3,
                    Name = "Probiotics",
                    Description = "Probiotics promote a healthy balance of gut bacteria.",
                    Interactions = "May interact with immunosuppressive medications.",
                    Precautions = "Choose strains with scientific support for desired effects.",
                    BestTimeToTake = "Best taken on an empty stomach."
                },
                new Supplement
                {
                    Id = 4,
                    Name = "Iron",
                    Description = "Iron is important for the production of red blood cells.",
                    Interactions = "May interact with certain antibiotics and antacids.",
                    Precautions = "Should not be taken with calcium-rich foods or supplements.",
                    BestTimeToTake = "Take on an empty stomach with vitamin C for better absorption."
                },
                new Supplement
                {
                    Id = 5,
                    Name = "Magnesium",
                    Description = "Magnesium is essential for nerve and muscle function.",
                    Interactions = "May interact with certain medications for heart and bones.",
                    Precautions = "Avoid excessive magnesium intake, as it can be toxic.",
                    BestTimeToTake = "Can be taken with meals to reduce gastrointestinal symptoms."
                }
            );

            //Exam
            modelBuilder.Entity<Exam>().HasData(
                new Exam
                {
                    Id = 1,
                    Name = "Blood Panel",
                    Description = "A comprehensive blood test that provides information about your health.",
                    Prerequisite = "Fasting for at least 8 hours before the test."
                },
                new Exam
                {
                    Id = 2,
                    Name = "Cholesterol Test",
                    Description = "Measures your levels of different types of cholesterol in the blood.",
                    Prerequisite = "Fasting for at least 9 to 12 hours before the test."
                },
                new Exam
                {
                    Id = 3,
                    Name = "Thyroid Function Test",
                    Description = "Evaluates how well your thyroid gland is functioning.",
                    Prerequisite = "May require fasting and avoiding certain medications before the test."
                },
                new Exam
                {
                    Id = 4,
                    Name = "Electrocardiogram (ECG or EKG)",
                    Description = "Records the electrical activity of your heart over a period of time.",
                    Prerequisite = "No special preparations required."
                },
                new Exam
                {
                    Id = 5,
                    Name = "Bone Density Test",
                    Description = "Measures bone mineral density to assess the risk of osteoporosis.",
                    Prerequisite = "No special preparations required."
                }
            );

            //Food

            modelBuilder.Entity<Food>().HasData(
                new Food
                {
                    Id = 1,
                    Name = "Salmon",
                    Description = "Salmon is a fatty fish rich in omega-3 fatty acids.",
                    Interactions = "May interact with blood-thinning medications."
                },
                new Food
                {
                    Id = 2,
                    Name = "Broccoli",
                    Description = "Broccoli is a cruciferous vegetable known for its health benefits.",
                    Interactions = "No significant interactions reported."
                },
                new Food
                {
                    Id = 3,
                    Name = "Blueberries",
                    Description = "Blueberries are packed with antioxidants and nutrients.",
                    Interactions = "No significant interactions reported."
                },
                new Food
                {
                    Id = 4,
                    Name = "Almonds",
                    Description = "Almonds are a nutritious and protein-rich nut.",
                    Interactions = "May interact with medications that lower blood sugar."
                },
                new Food
                {
                    Id = 5,
                    Name = "Spinach",
                    Description = "Spinach is a leafy green vegetable packed with vitamins and minerals.",
                    Interactions = "May interact with blood-thinning medications."
                }
            );

            //Lifestyle
            modelBuilder.Entity<Lifestyle>().HasData(
                new Lifestyle
                {
                    Id = 1,
                    Name = "Regular Exercise",
                    Description = "Engaging in regular physical activity has numerous health benefits.",
                    Interactions = "May interact with certain medical conditions and medications.",
                    Precautions = "Consult a healthcare professional before starting a new exercise program."
                },
                new Lifestyle
                {
                    Id = 2,
                    Name = "Healthy Diet",
                    Description = "Eating a balanced and nutritious diet supports overall well-being.",
                    Interactions = "May interact with certain medical conditions and medications.",
                    Precautions = "Consult a registered dietitian for personalized dietary guidance."
                },
                new Lifestyle
                {
                    Id = 3,
                    Name = "Stress Management",
                    Description = "Effective stress management techniques promote mental and emotional health.",
                    Interactions = "May interact with mental health conditions and medications.",
                    Precautions = "Explore relaxation techniques like meditation and deep breathing."
                },
                new Lifestyle
                {
                    Id = 4,
                    Name = "Adequate Sleep",
                    Description = "Getting enough quality sleep is essential for physical and mental recovery.",
                    Interactions = "May interact with sleep disorders and certain medications.",
                    Precautions = "Prioritize a consistent sleep schedule and create a sleep-conducive environment."
                },
                new Lifestyle
                {
                    Id = 5,
                    Name = "Tobacco-Free",
                    Description = "Avoiding tobacco products reduces the risk of various health issues.",
                    Interactions = "Tobacco interacts negatively with almost all bodily systems.",
                    Precautions = "Seek professional help if you want to quit smoking or using tobacco."
                }
            );

            //Meal
            modelBuilder.Entity<Meal>().HasData(
                new Meal
                {
                    Id = 1,
                    Name = "Grilled Chicken Salad",
                    Description = "A healthy salad with grilled chicken, mixed greens, and assorted vegetables.",
                    PreparationMethod = "Grill the chicken and toss it with fresh vegetables and dressing.",
                    TotalCalories = 350
                },
                new Meal
                {
                    Id = 2,
                    Name = "Vegetable Stir-Fry",
                    Description = "A colorful stir-fry with a variety of vegetables and tofu.",
                    PreparationMethod = "Stir-fry the vegetables and tofu with your favorite sauce.",
                    TotalCalories = 300
                },
                new Meal
                {
                    Id = 3,
                    Name = "Salmon with Quinoa",
                    Description = "Baked salmon served with a side of quinoa and steamed vegetables.",
                    PreparationMethod = "Season the salmon, bake it, and serve with cooked quinoa.",
                    TotalCalories = 400
                },
                new Meal
                {
                    Id = 4,
                    Name = "Oatmeal Breakfast Bowl",
                    Description = "A hearty breakfast bowl with oats, berries, nuts, and yogurt.",
                    PreparationMethod = "Cook oats, top with berries, nuts, and yogurt.",
                    TotalCalories = 250
                },
                new Meal
                {
                    Id = 5,
                    Name = "Spaghetti with Marinara Sauce",
                    Description = "Classic spaghetti dish with tomato marinara sauce and grated cheese.",
                    PreparationMethod = "Cook spaghetti and top with marinara sauce and cheese.",
                    TotalCalories = 350
                }
            );

            //MeasurementUnit
            modelBuilder.Entity<MeasurementUnit>().HasData(
                new MeasurementUnit
                {
                    Id = 1,
                    Name = "Gram",
                    Acronym = "g",
                    Description = "A metric unit of mass."
                },
                new MeasurementUnit
                {
                    Id = 2,
                    Name = "Milliliter",
                    Acronym = "ml",
                    Description = "A metric unit of volume."
                },
                new MeasurementUnit
                {
                    Id = 3,
                    Name = "Cup",
                    Acronym = "cup",
                    Description = "A customary unit of volume."
                },
                new MeasurementUnit
                {
                    Id = 4,
                    Name = "Teaspoon",
                    Acronym = "tsp",
                    Description = "A customary unit of volume for small amounts of liquids."
                },
                new MeasurementUnit
                {
                    Id = 5,
                    Name = "Tablespoon",
                    Acronym = "tbsp",
                    Description = "A customary unit of volume for larger amounts of liquids."
                },
                new MeasurementUnit
                {
                    Id = 6,
                    Name = "Drop",
                    Acronym = "drop",
                    Description = "A small amount of liquid typically dispensed by dropper."
                },
                new MeasurementUnit
                {
                    Id = 7,
                    Name = "Tablet",
                    Acronym = "tab",
                    Description = "A solid dose of medication typically in a flat, round shape."
                },
                new MeasurementUnit
                {
                    Id = 8,
                    Name = "Pill",
                    Acronym = "pill",
                    Description = "A solid dose of medication typically in a small, cylindrical shape."
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}