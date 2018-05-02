using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ToucanTesting.Api.Controllers.TestIssues;
using ToucanTesting.Controllers.ExpectedResults;
using ToucanTesting.Controllers.TestActions;
using ToucanTesting.Controllers.TestConditions;
using ToucanTesting.Models;

namespace ToucanTesting.Controllers.TestCases
{
    public class TestCaseResource
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Description { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public bool IsAutomated { get; set; }
        [StringLength(4)]
        public string AutomationId { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
        public DateTime? LastTested { get; set; }
        [Required]
        public long TestModuleId { get; set; }
        public ICollection<TestActionResource> TestActions { get; set; }
        public ICollection<TestConditionResource> TestConditions { get; set; }
        public ICollection<ExpectedResultResource> ExpectedResults { get; set; }
        public ICollection<TestIssueResource> TestIssues { get; set; }

        public TestCaseResource()
        {
            TestActions = new Collection<TestActionResource>();
            TestConditions = new Collection<TestConditionResource>();
            ExpectedResults = new Collection<ExpectedResultResource>();
            TestIssues = new Collection<TestIssueResource>();
            
        }

    }
}