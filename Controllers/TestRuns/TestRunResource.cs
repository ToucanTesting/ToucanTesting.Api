using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TucanTesting.Controllers.TestModules;
using TucanTesting.Controllers.TestResults;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestSuites
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