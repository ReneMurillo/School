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
            var AvgSubjectList = reporter.GetAvgStudentXSubject();
            var test = reporter.GetBestAverageXSubject(5);

            Printer.WriteTitle("Capture an evaluation by console");
            var newEval = new Evaluation();
            string name, scoreString;
            WriteLine("Enter the name of the evaluation");
            Printer.PressEnter();
            name = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(name))
            {
                Printer.WriteTitle("The value of name can't be empty");
                Printer.ExitingProgram();
            }
            else
            {
                newEval.Name = name.ToLower();
                WriteLine("Name has been saved correctly");
            }

            WriteLine("Enter the score of the evaluation");
            Printer.PressEnter();
            scoreString = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(scoreString))
            {
                Printer.WriteTitle("The value of the score can't be empty");
                Printer.ExitingProgram();
            }
            else
            {
                try
                {
                    newEval.Score = float.Parse(scoreString);
                    if(newEval.Score < 0 || newEval.Score > 5)
                        throw new ArgumentOutOfRangeException("The score must be a value from 0 to 5");
                    WriteLine("The score has been saved correctly");
                }
                catch(ArgumentOutOfRangeException arge)
                {
                    WriteLine(arge.Message);
                }
                catch(Exception)
                {
                    Printer.WriteTitle("The value of the score isn't valid");
                    Printer.ExitingProgram();
                }
                finally
                {
                    Printer.WriteTitle("FINALLY");
                    Printer.Beeper(2500,500,3);
                }
                
            }
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
