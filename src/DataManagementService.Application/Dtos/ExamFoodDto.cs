using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class ExamFoodDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int ExamId { get; set; }
        public ExamResult ExamResult { get; set; }
        public PatientAction Action { get; set; }
        
        public Food Food { get; set; }
        public Exam Exam { get; set; }
    }
}