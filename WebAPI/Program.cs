using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
// ConfigureServices() **************************
builder.Services.AddControllers();


builder.Services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new() { Title = "Basic Web API", Version = "v1" });
        });
// **********************************************
var app = builder.Build();
// Configure() **********************************
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Application v1"));
}

app.MapControllers();

// **********************************************
app.Run();
