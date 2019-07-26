using System;
using System.Collections.Generic;

namespace CoreSchool.Entities
{  
    public class Student: SchoolObjectBase
    {
        public List<Evaluations> Evaluations {get; set;} = new List<Evaluations>();

    }
}