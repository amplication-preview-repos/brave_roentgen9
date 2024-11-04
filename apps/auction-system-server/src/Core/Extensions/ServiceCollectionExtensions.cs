using AuctionSystem.APIs;

namespace AuctionSystem;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAuctionsService, AuctionsService>();
        services.AddScoped<IBidsService, BidsService>();
        services.AddScoped<IBikesService, BikesService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
