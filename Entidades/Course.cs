using System;
using System.Collections.Generic;

namespace CoreSchool.Entities
{
    public class Course: SchoolObjectBase
    {
        public JourneyType Journey { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Student> Students { get; set; }

    }
}