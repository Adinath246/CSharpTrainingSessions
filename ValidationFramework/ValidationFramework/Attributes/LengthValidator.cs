using System;
namespace ValidationFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LengthValidator : ValidationAttribute
    {
        int _maxLength;

        public LengthValidator(int maxLength)
        {
            _maxLength = maxLength;
        }

        public override bool Validate(object data)
        {
            int value = data?.ToString()?.Length ?? 0;
            return _maxLength <= value;
        }
    }
}

