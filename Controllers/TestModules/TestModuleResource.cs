using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ToucanTesting.Controllers.TestCases;
using ToucanTesting.Models;

namespace ToucanTesting.Controllers.TestModules
{
    public class TestModuleResource
    {
        public long Id { get; set; }
        [Required]
        public long TestSuiteId { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
        public ICollection<TestCaseResource> TestCases { get; set; }
        public TestModuleResource()
        {
            TestCases = new Collection<TestCaseResource>();
        }

    }
}