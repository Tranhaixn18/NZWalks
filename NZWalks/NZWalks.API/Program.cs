using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Data;
using NZWalks.API.Profiles;
using NZWalks.API.Repositories;
using NZWalks.API.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//install package fluentValidation,fluentValidation.asp.netcore,fluentValidation.dêpndencyInjectorExtention
// truyen AddRegionRequestValidator se chi dinh cu the,con truyen program thi se lay tat ca lien quan den validator
builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<AddRegionRequestValidator>());

builder.Services.AddDbContext<NZWalkDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});
builder.Services.AddScoped<IRegionRepository,RegionRepository>();
builder.Services.AddAutoMapper(typeof(RegionProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
