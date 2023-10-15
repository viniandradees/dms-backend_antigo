using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseExamDto
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int ExamId { get; set; }
        public ExamResult ExamResult { get; set; }

        public Disease Disease { get; set;}
        public Exam Exam { get; set; }
    }
}