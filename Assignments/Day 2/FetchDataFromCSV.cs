using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PatientInfo
{
    //constructor
    public PatientInfo(string mrn)
    {
        this.mrn = mrn;
    }

    //Backing Field - Memory
    private string mrn;
    //Public Property
    public string MRN { get { return this.mrn; } }


    //Auto implemented Properties
    public string Name { get; set; }
    public int Age { get; set; }
    public string ContactNumber { get; set; }

    //Public Field
    public string Email;
}

public static class ExtensionMethods
{
    public static string Dump(this PatientInfo patientInfo)
    {
        return $"{patientInfo.MRN},{patientInfo.Name},{patientInfo.Age},{patientInfo.ContactNumber},{patientInfo.Email}";
    }
}

public class PatientCSVProvider
{
    public string FilePath { get; set; }

    public List<PatientInfo> GetAllPatients()
    {
        return (from csvFile in File.ReadAllLines(FilePath).Skip(1)
                let line = csvFile.Split(',')
                select new PatientInfo(line[0])
                {
                    //MRN = line[0],
                    Name = line[1],
                    Age = int.Parse(line[2]),
                    ContactNumber = line[3],
                    Email = line[4]
                }).ToList();   
    }
}

public class Client
{
    //public void Query()
    public static void Main(string[] args)
    {
        PatientCSVProvider provider = new PatientCSVProvider();
        provider.FilePath = $"/Users/adinathbajpai/Projects/Training/Training/patient.csv";
        IEnumerable<PatientInfo> patients = provider.GetAllPatients();
        IEnumerable<PatientInfo> result = patients.Where(p => p.Age > 30);
        foreach (PatientInfo patient in result)
        {
            Console.WriteLine(patient.Dump()); // M400,JAMES,35,12356,james@tom.com
                                               //M500,JACOB,38,12346,jacob @tom.com
        }
    }
}
