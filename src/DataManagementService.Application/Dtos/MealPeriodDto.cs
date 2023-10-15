using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class MealPeriodDto
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        
        public MealTime MealTime { get; set; }
        public Meal Meal { get; set; }
    }
}