
var builder = WebApplication.CreateBuilder(args);

// Adding Services.
builder.Services.AddCors(options => {
	options.AddPolicy("AllowOrigin", 
		builder => builder
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keeps PascalCase
        options.JsonSerializerOptions.WriteIndented = true; // Pretty print (optional)
    });

var app = builder.Build();

// Configure the HTTP request pipe-line
if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();
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