using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class MealFoodDto
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int FoodId { get; set; }
        public decimal FoodPortion { get; set; }
        public int MeasurementUnitId { get; set; }
        
        public Meal Meal { get; set; }
        public Food Food { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
    }
}