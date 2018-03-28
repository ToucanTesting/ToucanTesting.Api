using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TucanTesting.Controllers.ExpectedResults;
using TucanTesting.Controllers.TestActions;
using TucanTesting.Controllers.TestConditions;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestCases
{
    public class TestCaseResource
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        public Priority Priority { get; set; }
        [Required]
        public bool IsAutomated { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
        public string BugId { get; set; }
        public long TestModuleId { get; set; }
        public DateTime? LastTested { get; set; }
        public ICollection<TestActionResource> TestActions { get; set; }
        public ICollection<TestConditionResource> TestConditions { get; set; }
        public ICollection<ExpectedResultResource> ExpectedResults { get; set; }

        public TestCaseResource()
        {
            TestActions = new Collection<TestActionResource>();
            TestConditions = new Collection<TestConditionResource>();
            ExpectedResults = new Collection<ExpectedResultResource>();
        }

    }
}