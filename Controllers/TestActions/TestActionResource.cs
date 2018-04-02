using System.ComponentModel.DataAnnotations;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestActions
{
    public class TestActionResource
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