using MyShop.Domain.Models;
using MyShop.Infrastructure;

namespace MyShop.Web
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
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddRazorPages();

      CreateInitialDatabase();

      services.AddTransient<ShoppingContext>();

    }

    public void CreateInitialDatabase()
    {
      using (var context = new ShoppingContext())
      {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var camera = new Product { Name = "Canon EOS 70D", Price = 599m };
        var microphone = new Product { Name = "Shure SM7B", Price = 245m };
        var light = new Product { Name = "Key Light", Price = 59.99m };
        var phone = new Product { Name = "Android Phone", Price = 259.59m };
        var speakers = new Product { Name = "5.1 Speaker System", Price = 799.99m };

        context.Products.Add(camera);
        context.Products.Add(microphone);
        context.Products.Add(light);
        context.Products.Add(phone);
        context.Products.Add(speakers);

        context.SaveChanges();
      }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Order/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Order}/{action=Index}/{id?}"
        );
        endpoints.MapRazorPages();
      });
    }
  }
}