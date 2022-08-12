using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Training
{
    public class PropertyContainer : DynamicObject
    {
        public string Line { get; set; }
        public string Header { get; set; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            string propertyName = binder.Name;
            List<string> headerList = Header.Split(',').ToList();
            int position = headerList.IndexOf(propertyName);
            if (position >= 0)
            {
                List<string> columnList = Line.Split(',').ToList();
                result = columnList[position];

                return true;
            }
            return false;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            if (binder.Name == "Dump")
            {
                return true;
            }
            return false;
        }
    }

    public class CSVProvider
    {
        public string FilePath { get; set; }

        public List<PropertyContainer> GetAllRecords()
        {
            var csvFile = File.ReadAllLines(FilePath);
            List<PropertyContainer> csvContent = new List<PropertyContainer>();
            for (int i = 1; i < csvFile.Length; i++)
            {
                PropertyContainer line = new PropertyContainer
                {
                    Header = csvFile[0],
                    Line = csvFile[i]
                };
                csvContent.Add(line);
            }
            return csvContent;
        }
    }

    //internal class DynamicProgramming
    //{
    //    static void Main()
    //    {
    //        dynamic _patientInfo = new PropertyContainer(); // object _patientInfo = new PropertyContainer();

    //        // Generate Binder Class - Evaluate/Resolve @Runtime
    //        _patientInfo.MRN = "M100";//Property Setter Expression
    //        _patientInfo.Name = "Tom";
    //        _patientInfo.Dump();//Method Invoke Expression

    //        dynamic _appointment = new PropertyContainer();
    //        _appointment.MRN = "M100";
    //        _appointment.Date = "20/8/2022";
    //        _appointment.Time = "20:30";
    //        _appointment.Dump();
    //    }
    //}
}
