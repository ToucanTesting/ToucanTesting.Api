using System.ComponentModel.DataAnnotations;

namespace ToucanTesting.Models
{
    public enum TestResultStatus { Pass, Fail, CNT, NA, Pending }
    public class TestResult : BaseEntity
    {
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