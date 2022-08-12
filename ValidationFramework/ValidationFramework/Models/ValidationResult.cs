using System;
namespace ValidationFramework.Models
{
    public class ValidationResult
    {
        public string PropertyName { get; set; }

        public IEnumerable<ValidationRule> ValidationRules { get; set; }
    }
}

