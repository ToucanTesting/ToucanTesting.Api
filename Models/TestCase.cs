using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TucanTesting.Models
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
        [Required]
        public bool IsEnabled { get; set; }
        public string BugId { get; set; }
        public DateTime? LastTested { get; set; }
        [Required]
        public long TestModuleId { get; set; }
        public TestModule TestModule { get; set; }
        public ICollection<TestAction> TestActions { get; set; }
        public ICollection<TestCondition> TestConditions { get; set; }
        public ICollection<ExpectedResult> ExpectedResults { get; set; }
        public TestCase()
        {
            TestActions = new Collection<TestAction>();
            TestConditions = new Collection<TestCondition>();
            ExpectedResults = new Collection<ExpectedResult>();
        }
    }
}