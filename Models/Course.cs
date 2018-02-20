using System;
using System.Collections.Generic;

namespace MultivisionCoreAPI.Models
{
    public class Course
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool Featured { get; set; }
        public DateTime Published { get; set; }
        public List<CourseTag> Tags { get; set; }
    }
}