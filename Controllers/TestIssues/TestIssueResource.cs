using System.ComponentModel.DataAnnotations;
using ToucanTesting.Controllers.TestCases;

namespace ToucanTesting.Api.Controllers.TestIssues
{
    public class TestIssueResource
    {
        public long Id { get; set; }
        [Required]
        public long TestCaseId { get; set; }
        public long TestRunId? { get; set; }
        [Required]
        [StringLength(16)]
        [MinLength(3)]
        public string Reference { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Description { get; set; }

    }
}
