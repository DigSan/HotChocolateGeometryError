using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using NetTopologySuite.Geometries;
using HotChocolate.Types.Spatial;

namespace HotChocolateGeometryError
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddQueryType<GraphQlQuery>()
                .AddProjections()
                .AddSpatialProjections()
                .AddSpatialTypes();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }

        public class GraphQlQuery
        {
            public IEnumerable<Test> GetWellStatuses()
            {
                return new List<Test> { new Test(){ SomeThing = "sdfsdf", Geometry = new Point(1, 1)} };
            }
        }

        public class Test
        {
            public string SomeThing { get; set; }

            public Geometry Geometry { get; set; }
        }
    }
}
