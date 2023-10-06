using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});






// Add services to the container.

builder.Services.AddControllers();

//Json serializer as default

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCors();
//services cors
//builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
//{
  //  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
    app.UseRouting();
    app.UseAuthorization();

    app.UseCors();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
        RequestPath = "/Photos"
    });

    }
//app cors


//app.UseCors(prodCorsPolicy);

app.MapControllers();

app.Run();