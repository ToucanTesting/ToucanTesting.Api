using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TucanTesting.Controllers.TestCases;
using TucanTesting.Models;

namespace TucanTesting.Controllers.TestModules
{
    public class TestModuleResource
    {
        public long Id { get; set; }
        public long TestSuiteId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        
    }
}