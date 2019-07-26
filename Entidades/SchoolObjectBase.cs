using System;

namespace CoreSchool.Entities
{
    public abstract class SchoolObjectBase
    {
        public string UniqueId { get; set; }
        public string Name { get; set; }

        public SchoolObjectBase() 
        {
            UniqueId = Guid.NewGuid().ToString();
        }
    }
}