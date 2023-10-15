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
    [Table("exam_supplement")]
    public class ExamSupplement
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("supplement_id", Order = 2)]
        public int SupplementId { get; set; }
        
        [Column("exam_id", Order = 3)]
        public int ExamId { get; set; }

        [Column("exam_result", Order = 4)]
        public ExamResult ExamResult { get; set; }

        public Supplement Supplement { get; set; }
        public Exam Exam { get; set; }
        public IEnumerable<ExamSupplementDosage> Dosages { get; set; }
    }
}