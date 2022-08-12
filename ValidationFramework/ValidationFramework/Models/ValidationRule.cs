using System;

namespace ValidationFramework
{
    public class ValidationRule
    {
        public ValidationStatus Status { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum ValidationStatus
    {
        VALID,
        INVALID
    }
}

