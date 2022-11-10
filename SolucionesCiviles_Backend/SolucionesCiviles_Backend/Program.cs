global using SolucionesCiviles_Backend.Services.EmailService;
global using SolucionesCiviles_Backend.Models;

var builder = WebApplication.CreateBuilder(args);
string SCivilesPolicy = "SCivilesPolicy";

// Add services to the container.
/*
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200");
        });
});*/

builder.Services.AddCors(options => options.AddPolicy(name: SCivilesPolicy, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("AllowAnyOrigin");

/*app.UseCors(options =>
    options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());*/

app.UseCors(SCivilesPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
