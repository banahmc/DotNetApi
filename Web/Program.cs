using DotNetApi.Model;

namespace DotNetApi.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddModel()
                .AddWeb();

            var app = builder.Build();

            app.ConfigureWeb();

            app.Run();
        }
    }
}
