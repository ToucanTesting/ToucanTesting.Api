using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ToucanTesting.Api.Controllers.TestIssues;
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
        public ICollection<TestIssueResource> TestIssues { get; set; }
        public TestRunResource()
        {
            TestResults = new Collection<TestResultResource>();
            TestIssues = new Collection<TestIssueResource>();
        }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}