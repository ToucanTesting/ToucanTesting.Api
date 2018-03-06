using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TucanTesting.Models
{
    public class TestModule : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public long TestSuiteId { get; set; }
        public TestSuite TestSuite { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<TestCase> TestCases { get; set; }
        public TestModule()
        {
            TestCases = new Collection<TestCase>();
        }
    }
}