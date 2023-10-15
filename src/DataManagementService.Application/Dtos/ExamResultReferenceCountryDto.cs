using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class ExamResultReferenceCountryDto
    {
        public int Id { get; set; }
        public int ExamResultReferenceId { get; set; }
        public int CountryId { get; set; }
        public ExamResultReference ExamResultReference { get; set; }
        public Country Country { get; set; }
    }
}