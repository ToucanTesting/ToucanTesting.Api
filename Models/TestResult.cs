namespace TucanTesting.Models
{
    public enum TestResultStatus { Pass, Fail, CNT, NA, Pending }
    public class TestResult : BaseEntity
    {
        public long Id { get; set; }
        public TestResultStatus Status { get; set; }
        public long TestRunId { get; set; }
        public long TestCaseId { get; set; }
    }
}