var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(); 
builder.Services.AddOpenApi();
try
{
    builder.Services.AddTransient<ICardRepository,cardRepository>();
    builder.Services.AddTransient<IPaymentRepository, PayRepository>();
builder.Services.AddTransient<IPaymentDetails,PaymentDetails>();

}
catch(Exception e)
{
    Console.WriteLine($"Error registering service: {e.Message}");
}

//builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 5001; }); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionHandler( errorApp => 
    { 
        errorApp.Run(async context => 
        { 
            context.Response.StatusCode = 500; 
            await context.Response.WriteAsync("An unexpected error occurred."); 
        }
        );
    });
}

//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", () => "Hello from ASP.NET Core!");

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapControllers(); 
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
