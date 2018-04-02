using System.ComponentModel.DataAnnotations;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestResults
{
    public class TestResultResource
    {
        public long? Id { get; set; }
        [Required]
        public long TestRunId { get; set; }
        [Required]
        public long TestModuleId { get; set; }
        [Required]
        public long TestCaseId { get; set; }
        [Required]
        public TestResultStatus Status { get; set; }
    }
}