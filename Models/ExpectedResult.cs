using System.ComponentModel.DataAnnotations;

namespace TucanTesting.Models
{
    public class ExpectedResult : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        public long TestCaseId { get; set; }
    }
}