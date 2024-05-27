using CoursesAPI.Data;
using CoursesAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace CoursesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly coursesContext context;
        public CoursesController(coursesContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("GetCourses")]
        public async Task<List<Course>> GetCourses()
        {
            return await context.Courses.ToListAsync();
        }

        [HttpPut]
        [Route("UpdateCourse")]
        public ActionResult Update(Course course)
        {
           context.Update(course);
            context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("InsertCourse")]
        public ActionResult Insert([FromBody] Course course)
        {
            context.Add(course);
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("DeleteCourse/{id}")]
        public ActionResult Delete(int id)
        {
            var course = context.Courses.Find(id);
            context.Courses.Remove(course);
            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("GetCourse/{id}")]
        public Course GetCourse(int id)
        {
            var course =context.Courses.Find(id);
            return course;
        }

        [HttpPost]
        [Route("UplaodImage")]
         public Course Uplaodimage()
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
            var filePath = Path.Combine("C:\\Users\\Administrator\\CoursesAngular\\src\\assets\\Images", fileName);
            using(var stream = new FileStream(filePath,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Course course = new Course();
            course.Image = fileName;
            return course;
        }


        [HttpPost]
        [Route("InsertFullCourse")]
        public ActionResult<List<Course>> InsertCourse([FromForm] Course course, [FromForm] IFormFile image)
        {
            if(image != null || image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                var filePath = Path.Combine("Image", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                course.Image = fileName;
                context.Add(course);
                context.SaveChanges();
                return RedirectToAction(nameof(GetCourses));
                
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Search")]
        public ActionResult<List<Stdcourse>> Search(Search search)
        {
            var std = context.Stdcourses.Include(s=>s.Std).Include(c=> c.Course).AsQueryable();
            if (!string.IsNullOrEmpty(search.Coursename))
            {
               std = std.Where(s => s.Course.Coursename == search.Coursename);
            }
            if (!string.IsNullOrEmpty(search.Studentname))
            {
               std= std.Where(s => s.Std.Firstname == search.Studentname);
            }
            return std.ToList();
        }

        [HttpGet]
        [Route("GetAllCat")]
        public ActionResult<List<Category>> GetAllCat()
        {
            var cat = context.Categories.Include(c=> c.Courses).ToList();
            return Ok(cat);
        }


        [HttpGet]
        [Route("Weather/{city}")]

        public async Task<Weather> Weather(string city)
        {
            using(var http = new HttpClient())
            {
                var response = await 
                    http.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=56cc32c52de871a058e6b732d56fd298");
                var stringResult = await response.Content.ReadAsStringAsync();
                var weatherResult = JsonConvert.DeserializeObject<Weather>(stringResult);
                return weatherResult;
            }
        }
 

    }
}
