using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class MealCountryDto
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int CountryId { get; set; }
        
        public Meal Meal { get; set; }
        public Country Country { get; set; }
    }
}