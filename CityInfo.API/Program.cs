
var builder = WebApplication.CreateBuilder(args);

// Adding Services.
builder.Services.AddCors(options => {
	options.AddPolicy("AllowOrigin", 
		builder => builder.AllowAnyOrigin());
});

builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keeps PascalCase
        options.JsonSerializerOptions.WriteIndented = true; // Pretty print (optional)
    });

var app = builder.Build();

// Configure the HTTP request pipliene
if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();
else
	app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseStatusCodePages();

app.UseCors("AllowOrigin");

app.UseRouting();
app.MapControllers();