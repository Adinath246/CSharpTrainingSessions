using System;
namespace ValidationFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false)]
    public class RequiredValidator : ValidationAttribute
    {
        public override bool Validate(object data)
        {
            string? value = data?.ToString();
            return string.IsNullOrEmpty(value);
        }
    }
}

