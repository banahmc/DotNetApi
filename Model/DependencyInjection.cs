using DotNetApi.Model.Cqrs.PipelineBehaviors;
using DotNetApi.Model.DataAccess;
using DotNetApi.Model.Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetApi.Model
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModel(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining(typeof(Product));

                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(Product).Assembly);

            return services;
        }
    }
}
