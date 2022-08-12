using System;
namespace ValidationFramework.Attributes
{
    public abstract class ValidationAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
        public ValidationAttribute()
        {
            ErrorMessage = String.Empty;
        }

        public abstract bool Validate(object data);
    }
}

