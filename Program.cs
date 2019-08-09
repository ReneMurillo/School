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
            var engine = new SchoolEngine();
            engine.Initialize(); 

            PrintSchoolCourses(engine.School);

            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            dictionary.Add(10, "JuanK");
            dictionary.Add(23, "Lorem ipsum");

            foreach (var keyValPair in dictionary)
            {
                WriteLine($"Key: {keyValPair.Key}, Value: {keyValPair.Value}");
            }

            Printer.WriteTitle("Dictionary access");
            WriteLine(dictionary[23]);

            Printer.WriteTitle("Another Dictionary");
            var dict = new Dictionary<string, string>();
            dict["moon"] = "Celestial body that revolves around the earth";

            WriteLine(dict["moon"]);
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
