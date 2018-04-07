using System.ComponentModel.DataAnnotations;
using TucanTesting.Controllers.TestCases;

namespace TucanTesting.Api.Controllers.TestIssues
{
    public class TestIssueResource
    {
        public long Id { get; set; }

        [Required]
        public long TestCaseId { get; set; }
        [Required]
        [StringLength(16)]
        [MinLength(3)]

        public string Reference { get; set; }

        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Description { get; set; }

        public string TestRunName { get; set; }

        public string TestModuleName { get; set; }

        public string TestCaseDescription { get; set; }
    }
}
