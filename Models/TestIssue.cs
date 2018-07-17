using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ToucanTesting.Models
{
    public class TestIssue : BaseEntity
    {
        [Required]
        public long TestCaseId { get; set; }

        public long? TestRunId { get; set; }

        [Required]
        [StringLength(16)]
        [MinLength(3)]
        public string Reference { get; set; }

        [Required]
        [StringLength(255)]
        [MinLength(3)]
        public string Description { get; set; }

    }
}