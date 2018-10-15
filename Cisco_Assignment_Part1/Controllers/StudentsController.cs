using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace server.Controllers
{
    //[Authorize(Roles="Admin")]
    public class StudentsController : Controller
    {
        StudentManager sm = new StudentManager();
        private IHostingEnvironment _Env;
        public StudentsController(IHostingEnvironment envrnmt)
        {
            _Env = envrnmt;
        }

        // GET api/students
        [HttpGet]
        [Route("api/[controller]")]
        public IEnumerable<Student> Get()
        {
            var webRootInfo = _Env.WebRootPath;
            var file = System.IO.Path.Combine(webRootInfo, "lastaccess.txt");
            System.IO.File.WriteAllText(file, DateTime.Now.ToString());
            return sm.GetAll;
        }

        //// GET api/students/5
        //[HttpGet("{id}")]
        //public async Task<Student> Get(int id)
        //{
        //    return await GetAsync(id);
        //}
        //private Task<Student> GetAsync(int id)
        //{
        //    var student = Task.FromResult(sm.GetAll.FirstOrDefault(c => c.Id == id));
        //    if (student == null)
        //    {
        //        throw new HttpRequestException("student with id " + id.ToString() + " not located");
        //        //return new HttpResponseMessage(statusCode: System.Net.HttpStatusCode.NotFound);
        //    }
        //    return student;
        //}

        // by GPA
        //// GET api/students/5
        //[HttpGet("{id}"]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IEnumerable<Student>> Get(int id)
        {
            return await GetAsync(id);
        }
        private Task<IEnumerable<Student>> GetAsync(int id)
        {
            return Task.FromResult(sm.GetStudentsByID(id));
        }

        // add new or edit
        // POST api/students
        [HttpPost]
        [Route("api/[controller]/[action]")]
        [ActionName("Post01")]
        public async Task<StatusCodeResult> Post01([FromBody] Student s)
        {
            if (s == null)
            {
                return new Microsoft.AspNetCore.Mvc.BadRequestResult();
            }
            if (await PostAsyncPartOne(s))
            {
                return await PostAsyncPartTwo(s);
            }
            else
            {
                sm.AddStudent(s);
                //dbContext.Companies.Add(company);
                //await dbContext.SaveChangesAsync();
                return new StatusCodeResult(201); //created
            }
        }
        private Task<bool> PostAsyncPartOne(Student s)
        {
            return Task.FromResult(sm.GetAll.Any(_ => _.Id == s.Id));
        }
        private Task<StatusCodeResult> PostAsyncPartTwo(Student s)
        {
            if (sm.EditStudent(s))
            {
                return Task.FromResult(new StatusCodeResult(200)); //success
            }
            else
            {
                return Task.FromResult(new StatusCodeResult(404)); //not found
            }
        }

        // add new or edit
        [HttpPut]
        [Route("api/[controller]/[action]")]
        [ActionName("Put01")]
        public async Task<StatusCodeResult> Put01([FromBody] Student s)
        {
            if (s == null)
            {
                return new Microsoft.AspNetCore.Mvc.BadRequestResult();
            }
            if (await PostAsyncPartOne(s))
            {
                return await PostAsyncPartTwo(s);
            }
            else
            {
                sm.AddStudent(s);
                //dbContext.Companies.Add(company);
                //await dbContext.SaveChangesAsync();
                return new StatusCodeResult(201); //created
            }
        }

        // delete
        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Route("api/[controller]/{id}")]
        public async Task<StatusCodeResult> Delete(int id)
        {
            return await DeleteAsync(id);
        }
        private Task<StatusCodeResult> DeleteAsync(int id)
        {
            if (sm.DeleteStudent(id))
                return Task.FromResult(new StatusCodeResult(200));
            else
                return Task.FromResult(new StatusCodeResult(404));
        }
    }
}
