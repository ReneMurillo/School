using System;
using System.Collections.Generic;
using CoreSchool.Util;

namespace CoreSchool.Entities
{
    public class Course: SchoolObjectBase, IPlace
    {
        public JourneyType Journey { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Student> Students { get; set; }

        public string Address { get; set; }

        public void CleanPlace()
        {
            Printer.DrawLine();
            Console.WriteLine("Cleaning place");
            Console.WriteLine($"Course {Name} now is clean");
        }

    }
}