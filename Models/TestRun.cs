using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TucanTesting.Models
{
    public class TestRun : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public long TestSuiteId { get; set; }
        public TestSuite TestSuite { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
        public TestRun()
        {
            TestResults = new Collection<TestResult>();
        }
    }
}