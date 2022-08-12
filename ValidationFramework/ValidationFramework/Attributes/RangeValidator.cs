using System;
namespace ValidationFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeValidator : ValidationAttribute
    {
        int _lowerLimit, _upperLimit;

        public RangeValidator(int lowerLimit, int upperLimit)
        {
            _lowerLimit = lowerLimit;
            _upperLimit = upperLimit;
        }

        public override bool Validate(object data)
        {
            int value = (int)data;
            return !(_lowerLimit >= value && _upperLimit < value);
        }
    }
}

