using EntityFrameWrokCodefirstApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//check if the app isRunning in development environment
if (app.Environment.IsDevelopment())
{
    //if yes it enables swagger middleware
    app.UseSwagger(); //generate the swagger json
    app.UseSwaggerUI(); //provides the interactive Swagger Web UI
}

app.UseAuthorization();

app.MapControllers();

app.Run();
