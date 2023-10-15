using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Domain;
using DataManagementService.Domain.Identity;

namespace DataManagementService.Application.Helpers
{
    public class DataManagementServiceProfile : Profile
    {
        public DataManagementServiceProfile()
        {
            // identity
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            // dms entities
            CreateMap<Country, CountryDto>().ReverseMap();

            CreateMap<DietaryOption, DietaryOptionDto>().ReverseMap();
            CreateMap<DietaryOptionFoodAttribute, DietaryOptionFoodAttributeDto>().ReverseMap();
            
            CreateMap<Disease, DiseaseDto>().ReverseMap();
            CreateMap<DiseaseDrug, DiseaseDrugDto>().ReverseMap();
            CreateMap<DiseaseDrugDosage, DiseaseDrugDosageDto>().ReverseMap();
            CreateMap<DiseaseDrugDosageAgeRange, DiseaseDrugDosageAgeRangeDto>().ReverseMap();
            CreateMap<DiseaseSupplement, DiseaseSupplementDto>().ReverseMap();
            CreateMap<DiseaseSupplementDosage, DiseaseSupplementDosageDto>().ReverseMap();
            CreateMap<DiseaseSupplementDosageAgeRange, DiseaseSupplementDosageAgeRangeDto>().ReverseMap();
            CreateMap<DiseaseFood, DiseaseFoodDto>().ReverseMap();
            CreateMap<DiseaseFoodDosage, DiseaseFoodDosageDto>().ReverseMap();
            CreateMap<DiseaseFoodDosageAgeRange, DiseaseFoodDosageAgeRangeDto>().ReverseMap();
            CreateMap<DiseaseLifestyle, DiseaseLifestyleDto>().ReverseMap();
            CreateMap<DiseaseDisease, DiseaseDiseaseDto>().ReverseMap();
            CreateMap<DiseaseExam, DiseaseExamDto>().ReverseMap();
            
            CreateMap<Drug, DrugDto>().ReverseMap();
            CreateMap<DrugDisease, DrugDiseaseDto>().ReverseMap();

            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<ExamResultReference, ExamResultReferenceDto>().ReverseMap();
            CreateMap<ExamResultReferenceCountry, ExamResultReferenceCountryDto>().ReverseMap();
            CreateMap<ExamResultReferenceVariation, ExamResultReferenceVariationDto>().ReverseMap();
            CreateMap<ExamSupplement, ExamSupplementDto>().ReverseMap();
            CreateMap<ExamSupplementDosage, ExamSupplementDosageDto>().ReverseMap();
            CreateMap<ExamSupplementDosageAgeRange, ExamSupplementDosageAgeRangeDto>().ReverseMap();
            CreateMap<ExamFood, ExamFoodDto>().ReverseMap();
            CreateMap<ExamLifestyle, ExamLifestyleDto>().ReverseMap();

            CreateMap<Food, FoodDto>().ReverseMap();
            CreateMap<FoodDisease, FoodDiseaseDto>().ReverseMap();
            CreateMap<FoodSupplement, FoodSupplementDto>().ReverseMap();
            CreateMap<FoodHealtyObjective, FoodHealtyObjectiveDto>().ReverseMap();
            CreateMap<FoodRelatedAttribute, FoodRelatedAttributeDto>().ReverseMap();

            CreateMap<FoodAttribute, FoodAttributeDto>().ReverseMap();

            CreateMap<HealtyObjective, HealtyObjectiveDto>().ReverseMap();

            CreateMap<Lifestyle, LifestyleDto>().ReverseMap();
            CreateMap<LifestyleDisease, LifestyleDiseaseDto>().ReverseMap();

            CreateMap<Meal, MealDto>().ReverseMap();
            CreateMap<MealFood, MealFoodDto>().ReverseMap();
            CreateMap<MealPeriod, MealPeriodDto>().ReverseMap();
            CreateMap<MealCountry, MealCountryDto>().ReverseMap();
            CreateMap<MealDietaryOption, MealDietaryOptionDto>().ReverseMap();

            CreateMap<MeasurementUnit, MeasurementUnitDto>().ReverseMap();
            
            CreateMap<Supplement, SupplementDto>().ReverseMap();
            CreateMap<SupplementDisease, SupplementDiseaseDto>().ReverseMap();
        }
    }
}