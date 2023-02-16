using CatIstagram.Server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatIstagram.Server.Infratrucure
{
    public static class ApplicationBuilderExtention
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            var Survices = app.ApplicationServices.CreateScope();
            var dbContext = Survices.ServiceProvider.GetService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
