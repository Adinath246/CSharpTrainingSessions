using System;
using ValidationFramework.Attributes;

namespace ValidationClientSupported.Models
{
    public class PatientInfo
    {
        [RequiredValidator(ErrorMessage = "GUID is required")]
        public string MMR { get; } = Guid.NewGuid().ToString();

        [RequiredValidator(ErrorMessage = "Name is required")]
        [LengthValidator(15, ErrorMessage = "Length should be less than 15")]
        public string Name { get; set; }


        [RangeValidator(1, 100, ErrorMessage = "Age Value Must be with in range 1-100")]
        public int Age { get; set; }
    }
}

