using System.ComponentModel.DataAnnotations;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestResults
{
    public class TestResultResource
    {
        public long? Id { get; set; }
        public TestResultStatus Status { get; set; }
        public long TestRunId { get; set; }
        public long TestModuleId { get; set; }
        public long TestCaseId { get; set; }
    }
}