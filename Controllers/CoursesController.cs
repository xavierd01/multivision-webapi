using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MultivisionCoreAPI.Models;
using System.Linq;

namespace MultivisionCoreAPI.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly CourseContext _context;

        public CoursesController(CourseContext context)
        {
            _context = context;

            if (_context.Courses.Count() == 0)
            {
                var defaultCourses = new List<Course> {
                    new Course {Title = "C# for Sociopaths", Featured = true, Published = new DateTime(2017, 10,5), Tags = new List<CourseTag>(){new CourseTag("C#")}},
                    new Course {Title = "C# for Non-Sociopaths", Featured = true, Published = new DateTime(2017, 10, 12), Tags = new List<CourseTag>(){new CourseTag("C#")}},
                    new Course {Title = "Super Duper Expert C#", Featured = false, Published = new DateTime(2017, 10, 1), Tags = new List<CourseTag>(){new CourseTag("C#")}},
                    new Course {Title = "Visual Basic for Visual Basic Developers", Featured = false, Published = new DateTime(2017, 7, 12), Tags = new List<CourseTag>(){new CourseTag("VB")}},
                    new Course {Title = "Pedantic C++", Featured = true, Published = new DateTime(2016, 1, 1), Tags = new List<CourseTag>(){new CourseTag("C++")}},
                    new Course {Title = "JavaScript for People over 20", Featured = true, Published = new DateTime(2017, 10, 13), Tags = new List<CourseTag>(){new CourseTag("JS")}},
                    new Course {Title = "Maintainable Code for Cowards", Featured = true, Published = new DateTime(2016, 10, 1), Tags = new List<CourseTag>(){new CourseTag("Coding")}},
                    new Course {Title = "A Survival Guide to Code Reviews", Featured = true, Published = new DateTime(2013, 5, 1), Tags = new List<CourseTag>(){new CourseTag("Coding")}},
                    new Course {Title = "How to Job Hunt without Alerting your Boss", Featured = true, Published = new DateTime(2017, 10, 7), Tags = new List<CourseTag>(){new CourseTag("Misc")}},
                    new Course {Title = "How to Keep your Soul and go into Management", Featured = false, Published = new DateTime(2017, 8, 1), Tags = new List<CourseTag>(){new CourseTag("Management")}},
                    new Course {Title = "Telling Recruiters to Leave you Alone", Featured = false, Published = new DateTime(2017, 11, 1), Tags = new List<CourseTag>(){new CourseTag("Misc")}},
                    new Course {Title = "Writing Code that Doesn't Suck", Featured = true, Published = new DateTime(2017, 10, 12), Tags = new List<CourseTag>(){new CourseTag("Coding")}},
                    new Course {Title = "Code Reviews for Jerks", Featured = false, Published = new DateTime(2017, 10, 1), Tags = new List<CourseTag>(){new CourseTag("Coding")}},
                    new Course {Title = "How to Deal with Narcissistic Coworkers", Featured = true, Published = new DateTime(2005, 5, 1), Tags = new List<CourseTag>(){new CourseTag("Misc")}},
                    new Course {Title = "Death March Coding for Fun and Profit", Featured = true, Published = new DateTime(2001, 1, 1), Tags = new List<CourseTag>(){new CourseTag("Coding"), new CourseTag("Misc")}},
                };
                _context.Courses.AddRange(defaultCourses);
                _context.SaveChanges();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtRoute("GetCourse", new { id = course.Id}, course);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) 
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpGet]
        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        [HttpGet("{id}", Name = "GetCourse")]
        public IActionResult GetById(long id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null) 
            {
                return NotFound();
            }
            return new ObjectResult(course);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Course updatedCourse)
        {
            if (updatedCourse == null || updatedCourse.Id != id) 
            {
                return BadRequest();
            }

            var course = _context.Courses.FirstOrDefault(c => c.Id == updatedCourse.Id);
            if (course == null)
            {
                return NotFound();
            }

            course.Title = updatedCourse.Title;
            course.Featured = updatedCourse.Featured;
            course.Published = updatedCourse.Published;
            course.Tags = updatedCourse.Tags;

            _context.Courses.Update(course);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
