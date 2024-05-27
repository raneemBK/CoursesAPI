using System;
using System.Collections.Generic;

namespace CoursesAPI.Data
{
    public partial class Category
    {
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string? Categoryname { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
