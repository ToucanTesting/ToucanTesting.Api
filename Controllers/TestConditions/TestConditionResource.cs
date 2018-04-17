using System.ComponentModel.DataAnnotations;
using ToucanTesting.Models;

namespace ToucanTesting.Controllers.TestConditions
{
    public class TestConditionResource
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