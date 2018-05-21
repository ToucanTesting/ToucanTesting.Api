using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ToucanTesting.Models
{
    public enum Priority { Low, Medium, High, Critical }
    public class TestCase : BaseEntity
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
        [Required]
        public bool HasCriteria { get; set; }
        public DateTime? DisabledAt { get; set; }
        public DateTime? LastTested { get; set; }
        [Required]
        public long TestModuleId { get; set; }
        public TestModule TestModule { get; set; }
        public ICollection<TestAction> TestActions { get; set; }
        public ICollection<TestCondition> TestConditions { get; set; }
        public ICollection<ExpectedResult> ExpectedResults { get; set; }
        public ICollection<TestIssue> TestIssues { get; set; }
        public TestCase()
        {
            TestActions = new Collection<TestAction>();
            TestConditions = new Collection<TestCondition>();
            ExpectedResults = new Collection<ExpectedResult>();
            TestIssues = new Collection<TestIssue>();
        }
    }
}