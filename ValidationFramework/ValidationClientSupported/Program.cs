// See https://aka.ms/new-console-template for more information
using ValidationClientSupported.Models;
using ValidationFramework.Business;
using System.Linq;

Console.WriteLine("Hello, World!");

PatientInfo patient = new();
var results = ObjectValidator.Validate(patient);
foreach(var result in results)
{
    Console.WriteLine(result.PropertyName);
    result.ValidationRules.ToList().ForEach(validation => Console.WriteLine(validation.ErrorMessage));
}