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
    [Table("exam_food")]
    public class ExamFood
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("food_id", Order = 2)]
        public int FoodId { get; set; }
        
        [Column("exam_id", Order = 3)]
        public int ExamId { get; set; }

        [Column("exam_result", Order = 4)]
        public ExamResult ExamResult { get; set; }

        [Column("action", Order = 5)]
        public PatientAction Action { get; set; }
        
        public Food Food { get; set; }
        public Exam Exam { get; set; }
    }
}