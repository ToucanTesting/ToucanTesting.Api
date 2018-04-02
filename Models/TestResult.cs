using System.ComponentModel.DataAnnotations;

namespace TucanTesting.Models
{
    public enum TestResultStatus { Pass, Fail, CNT, NA, Pending }
    public class TestResult : BaseEntity
    {
        public long Id { get; set; }
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