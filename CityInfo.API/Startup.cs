﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options => {
                options.AddPolicy("AllowOrigin",
                builder => builder.AllowAnyOrigin());
            });

            services.AddMvc()
                .AddJsonOptions(o =>
                {
                    if (o.SerializerSettings.ContractResolver != null)
                    {
                        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                        castedResolver.NamingStrategy = null;
                    }
                })
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();
            app.UseCors(builder => {
                builder.WithOrigins("AllowAnyOrigin");
            });

            app.UseMvc();

            app.Run(async (context) =>
            {
                var message = DoSomething();
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(message);
            });
        }

        private string DoSomething()
        {
            return $"<h2>This is a Test API. ASP.NET Core 2.2</h2>" +
                      $"<div>" +
                      $"    <p>If you see this message the API is up and Running. <br/>" +
                      $"    Use Postman to intorigate this API. <br/></p>" +
                      $"    CORS Implemented." +
                      $"</div>";
        }
    }
}
