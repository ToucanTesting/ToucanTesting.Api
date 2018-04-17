using System.ComponentModel.DataAnnotations;

namespace ToucanTesting.Models
{
    public class TestAction : BaseEntity
    {
        // Action == Step
        public long Id { get; set; }
        [Required]
        public long TestCaseId { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Description { get; set; }
        [Required]
        public int Sequence { get; set; }
    }
}