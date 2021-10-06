namespace SimpleApi.Extensions
{
    public static class EndpointDefinitionExtensions
    {
        public static void AddEndpointDefinitions(this IServiceCollection services, params Type[]  scanMarkers)
        {
            var endpointDefintions = new List<IEndpointDefinition>();

            foreach (var marker in scanMarkers)
            {
                endpointDefintions.AddRange(
                   marker.Assembly.ExportedTypes
                        .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsAbstract)
                        .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()     
                );
            }
            foreach (var endpointDefinition in endpointDefintions)
            {
                endpointDefinition.DefineServices(services);
            }

            services.AddSingleton(endpointDefintions as IReadOnlyCollection<IEndpointDefinition>);
           
        }
        public static void UseEndpointDefinitions(this WebApplication app)
        {
            var defintions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();
            foreach (var endpointDefinition in defintions)
            {
                endpointDefinition.DefineEndpoints(app);
            }
        }
    }
}