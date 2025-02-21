using movie_finder.bl;
using movie_finder.bl.search;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(SearchProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SearchMovieQuery).Assembly));

builder.Services.AddScoped<ISearchMovieService, TMDbSearchMovieService>();

builder.Services.Configure<MovieFinderOptions>(builder.Configuration.GetSection("movie-finder"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
