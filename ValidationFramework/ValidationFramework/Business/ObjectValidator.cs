using System;
using System.Collections.Generic;
using System.Reflection;
using ValidationFramework.Attributes;
using ValidationFramework.Models;

namespace ValidationFramework.Business
{
    public class ObjectValidator
    {
        public static IEnumerable<ValidationResult> Validate(object source)
        {
            //Use reflection to find all properties
            Type classRef = source.GetType();
            var properties = classRef.GetProperties();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            foreach(var property in properties)
            {
                List<ValidationRule> validationRules =  new List<ValidationRule>();
                //Console.WriteLine(property.Name);
                var validationAttributes = property.GetCustomAttributes(typeof(ValidationAttribute), true) as ValidationAttribute[];
                if (validationAttributes != null && validationAttributes.Length > 0)
                {
                    foreach(var attribute in validationAttributes)
                    {
                        ValidationRule validationRule = new ValidationRule
                        {
                            Status = ValidationStatus.VALID
                        };
                        if (attribute.Validate(property.GetValue(source)))
                        {
                            validationRule.Status = ValidationStatus.INVALID;
                            validationRule.ErrorMessage = attribute.ErrorMessage;
                        }
                        validationRules.Add(validationRule);
                    }
                }
                validationResults.Add(new ValidationResult
                {
                    PropertyName = property.Name,
                    ValidationRules = validationRules
                });
            }
            return validationResults;
        }
    }
}

