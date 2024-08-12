using DigitalStore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.WebApi.Extensions
{
    public static class HostServiceExtension
    {
        public static IHost UpdateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var digitalStoreDbContext = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>())
                {
                    try
                    {
                        var pendingMigrationsCount = digitalStoreDbContext.Database.GetPendingMigrations().Count();
                        if (pendingMigrationsCount > 0) digitalStoreDbContext.Database.Migrate();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
