using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SonOfCod.Models;

namespace SonOfCod
{

    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddDbContext<SonOfCodContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<AdminUser, IdentityRole>()
                .AddEntityFrameworkStores<SonOfCodContext>()
                .AddDefaultTokenProviders();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseIdentity();
            app.UseStaticFiles();
            var newContext = app.ApplicationServices.GetService<SonOfCodContext>();
            AddTestData(newContext);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("We got lost in a field of jellyfish! Turn back!");
            });
        }

        private static void AddTestData(SonOfCodContext context)
        {
            var autoMarket1 = new Models.Market
            {
                Title = "We care about our cod and our customers",
                Image = "http://www.photographyblogger.net/wp-content/uploads/2013/07/16-fisherman.jpg",
                Content = "At Pacific Seafood, doing just enough to get the job done is not acceptable. Average doesn’t cut it. Even good isn't sufficient. We set a high standard for ourselves, and expect every team member in the company to meet or exceed our customer’s needs. This is what differentiates us from other companies in the marketplace.",

            };
            context.Markets.Add(autoMarket1);

            var autoMarket2 = new Models.Market
            {
                Title = "The next generation carries on the tradition.",
                Image = "http://i.dailymail.co.uk/i/pix/2015/05/29/09/292BB85700000578-0-image-a-30_1432889733502.jpg",
                Content = "Having saved up his money from selling fish, Frank risked all he had to start his third company, Pacific Seafood, with his son Dominic. This time Frank quickly distinguished himself from other stores in the area. Due to their success, Pacific was approached by Bay City Oysters Company, one of their competitors who was going bankrupt, and was asked to buy out the business. Within a few months Dominic got his first supermarket account, Bazaar Big C, which gave Pacific the resources to purchase two more companies.The Dulcich family knew their company would only survive if they worked harder than their competition.This passion led them to work for 31 years until they took their first vacation in 1972.Frank's grandson, also named Frank, had graduated from the University of Portland to join the family business as one of its 18 employees. From its humble beginnings in 1941, Pacific Seafood has grown to employ over 2500 people at over 35 facilities, with processing and distribution facilities in seven states. Although the company has grown in size, it is still family-owned and family-focused while always being dedicated to delivering the best seafood products and the best customer service anywhere.",

            };
            context.Markets.Add(autoMarket2);

            var autoMarket3 = new Models.Market
            {
                Title = "Sustainability",
                Image = "http://www.atuna.com/images/sfp.jpg",
                Content = "It is our responsibility to serve the oceans through a continuous commitment to sustainable fishing practices. Not only does this help protect the longevity of our oceans and the important ecological balance within them, but it means we can continue to serve your family great tasting seafood for generations to come.",

            };
            context.Markets.Add(autoMarket3);

            context.SaveChanges();
        }
    }
}
