using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Extensions;
using SimpleApi.Interfaces;
using SimpleApi.Models;
using SimpleApi.Repositories;

namespace SimpleApi.EndpointDefinitions
{
    public class StudentEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet ("/students", GetStudents);
            app.MapGet("/students/{id}", GetStudentById);
            app.MapPost ("/students", CreateStudent);
            app.MapPut ("/students/{id}", UpdateStudent);
            app.MapDelete("/students/{id}", DeleteStudent);
        }
        internal IResult GetStudents(IRepository<Student> repo)
        {
                return Results.Ok(repo.GetAll());
        }
        internal IResult GetStudentById(IRepository<Student> repo, int id)
        {
             var student = repo.GetById(id);
             return student is not null ? Results.Ok(student) : Results.NotFound();
        }
        internal IResult CreateStudent(IRepository<Student> repo, Student student)
        {
            repo.Insert(student);
            repo.Save();
            return Results.Created ($"/students/{student.StudentId}", student);
        }
        internal IResult UpdateStudent(IRepository<Student> repo, int id, Student updatedStudent)
        {
               var student = repo.GetById (id);
                if (student is null)
                    return Results.NotFound ();

                repo.Update(updatedStudent);
                repo.Save();
                return Results.Ok(updatedStudent);
        }
        internal IResult DeleteStudent(IRepository<Student> repo, int id)
        {
              var customer = repo.GetById(id);
                if (customer is null)
                    return Results.NotFound();

                repo.Delete(id);
                repo.Save();
                return Results.Ok();
        }
        public void DefineServices(IServiceCollection services)
        {
             services.AddScoped<IRepository<Student>,Repository<Student>>();
        }
    }
}