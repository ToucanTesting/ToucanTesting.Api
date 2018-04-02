using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TucanTesting.Models
{
    public class TestSuite : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
        public ICollection<TestModule> TestModules { get; set; }
        public TestSuite()
        {
            TestModules = new Collection<TestModule>();
        }
    }
}