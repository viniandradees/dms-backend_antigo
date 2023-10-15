using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class FoodHealtyObjectiveDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int HealtyObjectiveId { get; set; }
        
        public Food Food { get; set; }
        public HealtyObjective HealtyObjective { get; set; }
    }
}