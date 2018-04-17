using System.ComponentModel.DataAnnotations;

namespace ToucanTesting.Models
{
    public class TestCondition : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        public long TestCaseId { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Description { get; set; }
    }
}