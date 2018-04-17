using System.ComponentModel.DataAnnotations;
using ToucanTesting.Models;

namespace ToucanTesting.Controllers.ExpectedResults
{
    public class ExpectedResultResource
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