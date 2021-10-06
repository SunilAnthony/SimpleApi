using SimpleApi.Extensions;
using SimpleApi.Repositories;
using SimpleApi.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.Interfaces;

namespace SimpleApi.EndpointDefinitions {
    public class CustomerEndpointDefinition : IEndpointDefinition {
        public void DefineEndpoints (WebApplication app) {
            app.MapGet ("/customers", GetCustomers);
            app.MapGet("/customers/{id}", GetCustomerById);
            app.MapPost ("/customers", CreateCustomer);
            app.MapPut ("/customers/{id}", UpdateCustomer);
            app.MapDelete("/customers/{id}", DeleteCustomer);
        }

        internal IResult GetCustomers(IRepository<Customer> repo)
        {
                return Results.Ok(repo.GetAll());
        }
        internal IResult GetCustomerById(IRepository<Customer> repo, Guid id)
        {
             var customer = repo.GetById(id);
             return customer is not null ? Results.Ok(customer) : Results.NotFound();
        }
        internal IResult CreateCustomer(IRepository<Customer> repo, Customer customer)
        {
              repo.Insert(customer);
            repo.Save();
            return Results.Created ($"/customers/{customer.Id}", customer);
        }
        internal IResult UpdateCustomer(IRepository<Customer> repo, Guid id, Customer updatedCustomer)
        {
               var customer = repo.GetById (id);
                if (customer is null)
                    return Results.NotFound ();

                repo.Update(updatedCustomer);
                repo.Save();
            return Results.Ok (updatedCustomer);
        }
        internal IResult DeleteCustomer(IRepository<Customer> repo, Guid id)
        {
            var customer = repo.GetById(id);
            if (customer is null)
                return Results.NotFound();

            repo.Delete (id);
            repo.Save();
            return Results.Ok ();
        }

        public void DefineServices (IServiceCollection services) {
            services.AddScoped<IRepository<Customer>,Repository<Customer>>();
        }
    }
}