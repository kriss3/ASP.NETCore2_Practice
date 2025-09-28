
using CityInfo.API.Application;
using CityInfo.API.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Adding Services.
builder.Services.AddCors(options => {
	options.AddPolicy("AllowOrigin", 
		builder => builder
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});


builder.Services.AddDbContext<CityInfoContext>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddScoped<ICityInfoService, CityInfoService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keeps PascalCase
        options.JsonSerializerOptions.WriteIndented = true; // Pretty print (optional)
    });

WebApplication app = builder.Build();

// Configure the HTTP request pipe-line
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
	app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseStatusCodePages();

app.UseRouting();
app.UseCors("AllowOrigin");
app.MapControllers();

// Fallback endpoint
app.MapFallback(async context =>
{
	var message = DoSomething();
	context.Response.ContentType = "text/html";
	await context.Response.WriteAsync(message);
});

await SeedCityInfoDb(app);

app.Run();

static string DoSomething()
{
	return $"<h2>This is a Test API. ASP.NET Core/.NET 9</h2>" +
		   $"<div>" +
		   $"    <p>If you see this message the API is up and Running. <br/>" +
		   $"    Use Postman to interrogate this API. <br/></p>" +
		   $"    CORS Implemented." +
		   $"</div>";
}

static async Task SeedCityInfoDb(WebApplication app) 
{
	using var scope = app.Services.CreateScope();
	var db = scope.ServiceProvider.GetRequiredService<CityInfoContext>();
	await CityInfoSeeder.SeedAsync(db);          
}