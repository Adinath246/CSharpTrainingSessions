using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using Training;

public class PatientInfo
{
    public PatientInfo(string mrn)
    {
        _mrn = mrn;
    }

    private string _mrn;

    //Public Property
    public string MRN { get { return _mrn; } }

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
        var csvProvider = new CSVProvider();
        csvProvider.FilePath = FilePath;
        var csvContent = csvProvider.GetAllRecords();

        return (from csvLine in csvContent.Cast<dynamic>()
                select new PatientInfo(csvLine.MRN)
                {
                    Name = csvLine.NAME,
                    Age = int.Parse(csvLine.AGE),
                    ContactNumber = csvLine.CONTACTNUMBER,
                    Email = csvLine.EMAIL
                }).ToList();   
    }
}

public class Client
{
    //public void Query()
    public static void Main(string[] args)
    {
        PatientCSVProvider provider = new PatientCSVProvider();
        string fileName = @"patient.csv";
        string currentDirectory = Directory.GetCurrentDirectory();
        string[] fullFilePath = Directory.GetFiles(currentDirectory, fileName, SearchOption.AllDirectories);
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


//using System;
//using System.Collections.Generic;

//namespace Training
//{
//    internal class Reusability
//    {
//        static void Main(string[] args)
//        {
//            string[] names = { "Philips", "Royal", "Siemens", "Cerner" };

//            //Select name from  names where name ends with "s";
//            Func<string, bool> _functionCommandObj = EndsWith_Any_Predicate("S");
//            IEnumerable<string> resultList = Search<string>(names, _functionCommandObj);
//            //display
//            foreach (string item in resultList)
//            {
//                Console.WriteLine(item);
//            }

//            _functionCommandObj = StartsWith_Any_Predicate("S"); 
//            resultList = Search<string>(names, _functionCommandObj);
//            //display
//            foreach (string item in resultList)
//            {
//                Console.WriteLine(item);
//            }

//            int[] numbers = { 1, 2, 3, 4, 5, 8 };
//            Func<int, bool> _intArgBoolReturnCommandObj = IsEven;
//            IEnumerable<int> intResultList = Search<int>(numbers, _intArgBoolReturnCommandObj);

//            foreach (int item in intResultList)
//            {
//                Console.WriteLine(item);
//            }

//        }

//        static bool IsEven(int item)
//        {
//            return item % 2 == 0;
//        }

//        static Func<string, bool> StartsWith_Any_Predicate(string startWith)
//        {
//            return (string item) =>
//            {
//                return item.StartsWith(startWith);
//            };
//        }

//        static Func<string, bool> EndsWith_Any_Predicate(string endsWith)
//        {
//            bool EndsWith_Predicate(string item)
//            {
//                return item.EndsWith(endsWith);
//            }
//            return EndsWith_Predicate;
//        }

//        static IEnumerable<Tsource> Search<Tsource>(IEnumerable<Tsource> source, Func<Tsource, bool> predicate)
//        {
//            List<Tsource> resultList = new List<Tsource>();
//            //Select name from  names where name ends with "s";

//            foreach (Tsource item in source)
//            {
//                if (predicate.Invoke(item))
//                {
//                    resultList.Add(item);

//                }
//            }
//            return resultList;

//        }
//    }
//}


//using System;
//using System.Collections.Generic;

//namespace Training
//{
//    public delegate bool AnyArgType_ReturnBoolCommandClass<TSource>(TSource item);

//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            string[] names = { "Philips", "Royal", "Siemens", "Cerner" };

//            //Select name from  names where name ends with "s";
//            AnyArgType_ReturnBoolCommandClass<string> _functionCommandObj = EndsWith_Any_Predicate("S");//new AnyArgType_ReturnBoolCommandClass<string>(Program.Endswith_s_Predicate);
//            IEnumerable<string> resultList = Search<string>(names, _functionCommandObj);
//            //display
//            foreach (string item in resultList)
//            {
//                Console.WriteLine(item);
//            }

//            _functionCommandObj = StartsWith_Any_Predicate("S"); //new AnyArgType_ReturnBoolCommandClass<string>(Program.Starswith_S_Predicate);
//            resultList = Search<string>(names, _functionCommandObj);
//            //display
//            foreach (string item in resultList)
//            {
//                Console.WriteLine(item);
//            }

//            int[] numbers = { 1, 2, 3, 4, 5, 8 };
//            AnyArgType_ReturnBoolCommandClass<int> _intArgBoolReturnCommandObj = new AnyArgType_ReturnBoolCommandClass<int>(Program.IsEven);
//            IEnumerable<int> intResultList = Search<int>(numbers, _intArgBoolReturnCommandObj);

//            foreach (int item in intResultList)
//            {
//                Console.WriteLine(item);
//            }

//        }

//        static bool IsEven(int item)
//        {
//            return item % 2 == 0;
//        }

//        //static bool Endswith_s_Predicate(string item)
//        //{
//        //    return item.EndsWith("s");
//        //}
//        //static bool Endswith_r_Predicate(string item)
//        //{
//        //    return item.EndsWith("r");
//        //}
//        //static bool Starswith_P_Predicate(string item)
//        //{
//        //    return item.StartsWith("P");
//        //}
//        //static bool Starswith_S_Predicate(string item)
//        //{
//        //    return item.StartsWith("S");
//        //}

//        static AnyArgType_ReturnBoolCommandClass<string> StartsWith_Any_Predicate(string startWith)
//        {
//            return (string item) =>
//            {
//                return item.StartsWith(startWith);
//            };
//        }

//        static AnyArgType_ReturnBoolCommandClass<string> EndsWith_Any_Predicate(string endsWith)
//        {
//            //return (string item) =>
//            //{
//            //    return item.EndsWith(endsWith);
//            //};

//            bool EndsWith_Predicate(string item)
//            {
//                return item.EndsWith(endsWith);
//            }
//            return EndsWith_Predicate;
//        }

//        static IEnumerable<Tsource> Search<Tsource>(IEnumerable<Tsource> source, AnyArgType_ReturnBoolCommandClass<Tsource> predicate)
//        {
//            List<Tsource> resultList = new List<Tsource>();
//            //Select name from  names where name ends with "s";

//            //IEnumerator<Tsource> iterator= source.GetEnumerator();
//            // try
//            // {
//            //     while (iterator.MoveNext())
//            //     {
//            //         Tsource currentItem=iterator.Current;
//            //         if (predicate(currentItem))
//            //         {
//            //             resultList.Add(currentItem);
//            //         }
//            //     }
//            // }
//            // finally
//            // {
//            //     if(iterator is IDisposable)
//            //     {
//            //         iterator.Dispose();
//            //     }
//            // }

//            foreach (Tsource item in source)
//            {
//                if (predicate.Invoke(item))
//                {
//                    resultList.Add(item);

//                }
//            }
//            return resultList;

//        }
//    }
//}
