using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ToucanTesting.Models
{
    public class TestRun : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public long TestSuiteId { get; set; }
        public TestSuite TestSuite { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
        public ICollection<TestIssue> TestIssues { get; set; }
        public TestRun()
        {
            TestResults = new Collection<TestResult>();
            TestIssues = new Collection<TestIssue>();
        }
    }
}
