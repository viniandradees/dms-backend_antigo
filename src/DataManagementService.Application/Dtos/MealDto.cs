using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string PreparationMethod { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalCalories { get; set; }

        public IEnumerable<MealFood> Ingredients { get; set; }
        public IEnumerable<MealPeriod> MealPeriods { get; set; }
        public IEnumerable<MealCountry> InternationalCuisines { get; set; }
        public IEnumerable<MealDietaryOption> MealDietaryOptions { get; set; }
    }
}