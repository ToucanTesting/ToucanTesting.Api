using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ToucanTesting.Controllers.TestModules;
using ToucanTesting.Controllers.TestResults;
using ToucanTesting.Models;

namespace ToucanTesting.Controllers.TestSuites
{
    public class TestRunResource
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public long TestSuiteId { get; set; }
        public TestSuite TestSuite { get; set; }

        public ICollection<TestResultResource> TestResults { get; set; }
        public TestRunResource()
        {
            TestResults = new Collection<TestResultResource>();
        }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}