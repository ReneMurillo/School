using System;
using System.Collections.Generic;

namespace CoreSchool.Entities
{
    public class Course
    {
        public string UniqueId { get; private set; }
        public string Name { get; set; }
        public JourneyType Journey { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Student> Students { get; set; }
        public Course()
        {
            UniqueId = Guid.NewGuid().ToString();
        }
    }
}