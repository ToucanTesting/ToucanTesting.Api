using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TucanTesting.Controllers.TestModules;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestSuites
{
    public class TestSuiteResource
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}