﻿using movie_finder.bl;
using movie_finder.bl.search;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = builder.Configuration.Get<AppSettings>()
        ?? throw new Exception("Incorrect configuration section");
builder.Services.AddSingleton(appSettings);

builder.Services.AddAutoMapper(typeof(SearchProfile));

builder.Services.AddScoped<ISearchMovieService, TMDbSearchMovieService>();

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