using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Domain
{
    [Table("disease_exam")]
    public class DiseaseExam
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("disease_id", Order = 2)]
        public int DiseaseId { get; set; }

        [Column("exam_id", Order = 3)]
        public int ExamId { get; set; }

        [Required]
        [Column("exam_result", Order = 4)]
        public ExamResult ExamResult { get; set; }


        public Disease Disease { get; set; }
        public Exam Exam { get; set; }
    }
}