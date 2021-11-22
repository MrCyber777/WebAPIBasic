using DataStore.EF;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
// ConfigureServices() **************************
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddApiVersioning(options =>
{
    // ��������� ���������� ������������ ������ API �� ���������
    options.AssumeDefaultVersionWhenUnspecified = true;

    // ���������, ����� ��������� ������ ������������ �� ���������
    options.DefaultApiVersion = new ApiVersion(1,0);

    // ��������� ���������� � ���������� ������, ������� ��� ��������� ������
    options.ReportApiVersions = true;

    // ���������, ��� ����� ������ � ���������� ������ ������ API
    //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<DiscontinueVersion1ResourceFilter>();
//});


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
