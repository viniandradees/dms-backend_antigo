using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class MealDietaryOptionDto
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int DietaryOptionId { get; set; }
        
        public Meal Meal { get; set; }
        public DietaryOption DietaryOption { get; set; }
    }
}