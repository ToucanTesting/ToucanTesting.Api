using System.ComponentModel.DataAnnotations;
using ToucanTesting.Interfaces;

namespace ToucanTesting.Models
{
    public class TestCondition : BaseEntity, ISequential
    {
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
