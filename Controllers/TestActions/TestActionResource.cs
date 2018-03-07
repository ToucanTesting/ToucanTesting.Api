using System.ComponentModel.DataAnnotations;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestActions
{
    public class TestActionResource
    {
        public long Id { get; set; }
        public long TestCaseId { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        public int Sequence { get; set; }
    }
}