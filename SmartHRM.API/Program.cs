using SmartHRM.Repository.Connection;
using SmartHRM.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDIServices(builder.Configuration);

//Account


//Employee
builder.Services.AddScoped<EmployeeService>();


builder.Services.AddScoped<ContractService>();

builder.Services.AddScoped<DepartmentService>();

//Task
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<TaskDetailsService>();

//Enable CORS
builder.Services.AddCors(p =>
    p.AddPolicy("HUG_LOCAL", build =>
    {
        //build.WithOrigins("https://localhost:7034",
        //                  "http://localhost:5034",
        //                  "https://localhost:7289",
        //                  "http://localhost:5289",
        //                  "http://localhost:3000")
        //     .AllowAnyMethod()
        //     .AllowAnyHeader()
        //    .AllowCredentials()
        //    .SetIsOriginAllowed((hosts) => true);
        build.WithOrigins("*")
             .AllowAnyMethod()
             .AllowAnyHeader();
    })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("HUG_LOCAL");

app.UseAuthorization();

app.MapControllers();

app.Run();
