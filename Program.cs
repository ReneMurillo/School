using System;
using System.Collections.Generic;
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

            //Printer.Beeper(2000,500,3);

            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.WriteTitle("Polymorphism tests");

            var studentTest = new Student { Name = "Claire Underwood" };
            SchoolObjectBase obj = studentTest;
            Printer.WriteTitle("Student");
            WriteLine($"Student: {studentTest.Name}");
            WriteLine($"Student: {studentTest.UniqueId}");
            WriteLine($"Student: {studentTest.GetType()}");

            Printer.WriteTitle("Object");
            WriteLine($"Object: {obj.Name}");
            WriteLine($"Object: {obj.UniqueId}");
            WriteLine($"Object: {obj.GetType()}");

            var objDummy = new SchoolObjectBase() { Name = "Frank Underwood"};
            Printer.WriteTitle("SchoolObjectBase");
            WriteLine($"Student: {objDummy.Name}");
            WriteLine($"Student: {objDummy.UniqueId}");
            WriteLine($"Student: {objDummy.GetType()}");

            var evaluation = new Evaluations() { Name = "Math evaluation", Score=4.5f};
            Printer.WriteTitle("Evaluation");
            WriteLine($"Evaluation: {evaluation.Name}");
            WriteLine($"Evaluation: {evaluation.Score}");
            WriteLine($"Evaluation: {evaluation.UniqueId}");
            WriteLine($"Evaluation: {evaluation.GetType()}");

            obj = evaluation;
            Printer.WriteTitle("SchoolObjectBase");
            WriteLine($"Student: {evaluation.Name}");
            WriteLine($"Student: {evaluation.UniqueId}");
            WriteLine($"Student: {evaluation.GetType()}");

            studentTest = (Student) (SchoolObjectBase) evaluation;
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
