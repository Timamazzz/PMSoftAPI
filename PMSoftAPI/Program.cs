using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Repositories;
using Domain.AutoMapper;
using Domain.Services;
using PMSoftAPI.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

//EF
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

//Mapping
builder.Services.AddAutoMapper(typeof(AppMappingPresentationToDomain), typeof(AppMappingDomainToDataAccess));


//Services
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<PublisherService>();
builder.Services.AddScoped<UserService>();

//Repositories
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<CountryRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<PublisherRepository>();
builder.Services.AddScoped<UserRepository>();

// Add services to the container.
builder.Services.AddControllers();

//Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();