using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.App;
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

            Printer.WriteTitle("Welcome to the School");

            var reporter = new Reporter(engine.GetObjectDictionary());
            var evaList = reporter.GetEvaluationsList();
            var subjList = reporter.GetSubjectsList();
            var evalXSubj = reporter.GetEvaluationsXSubjectDictionary();
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
