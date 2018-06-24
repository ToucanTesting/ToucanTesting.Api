using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ToucanTesting.Interfaces;

namespace ToucanTesting.Models
{
    public class TestModule : BaseEntity, ISequential
    {
        public long Id { get; set; }
        [Required]
        public long TestSuiteId { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public int Sequence { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
        public ICollection<TestCase> TestCases { get; set; }
        public DateTime? DisabledAt { get; set; }
        public TestModule()
        {
            TestCases = new Collection<TestCase>();
        }
    }
}