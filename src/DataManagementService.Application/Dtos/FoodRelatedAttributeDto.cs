using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class FoodRelatedAttributeDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int FoodAttributeId { get; set; }
        
        public Food Food { get; set; }
        public FoodAttribute FoodAttribute { get; set; }
    }
}