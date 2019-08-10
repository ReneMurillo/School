using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.Entities;
using CoreSchool.Util;
using static System.Console;

namespace CoreSchool
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += EventAction;
            var engine = new SchoolEngine();
            engine.Initialize(); 

            //PrintSchoolCourses(engine.School);

            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            dictionary.Add(10, "JuanK");
            dictionary.Add(23, "Lorem ipsum");

            foreach (var keyValPair in dictionary)
            {
                WriteLine($"Key: {keyValPair.Key}, Value: {keyValPair.Value}");
            }

            var dictmp = engine.GetObjectDictionary();
            engine.PrintDictionary(dictmp, true);
        }

        private static void EventAction(object sender, EventArgs e)
        {
            Printer.WriteTitle("Comming out");
            Printer.Beeper(3000,1000,3);
            Printer.WriteTitle("It left");
        }

        private static void PrintSchoolCourses(School school)
        {
            Printer.WriteTitle("School's Courses");

            if(school?.Courses != null)
            {
                foreach (var course in school.Courses)
                {
                    WriteLine($"Name {course.Name}, Id {course.UniqueId}");
                }
            }
        }
    }
}
