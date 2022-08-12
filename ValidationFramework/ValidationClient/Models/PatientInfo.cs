using System;
namespace ValidationClient.Models
{
    public class PatientInfo
    {
        public string MMR { get; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public int Age { get; set; }
    }
}

