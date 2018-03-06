using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TucanTesting.Controllers.TestModules;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestSuites
{
    public class TestRunResource
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public long TestSuiteId { get; set; }
        public TestSuite TestSuite { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}