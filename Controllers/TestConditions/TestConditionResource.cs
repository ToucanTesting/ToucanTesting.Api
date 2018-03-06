using System.ComponentModel.DataAnnotations;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestConditions
{
    public class TestConditionResource
    {
        public long Id { get; set; }
        public long TestCaseId { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}