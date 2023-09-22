using DBFirstLibrary;
using library_webapi_ef_tutorial.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    // See https://stackoverflow.com/questions/74246482/system-notsupportedexception-serialization-and-deserialization-of-system-dateo
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<libraryef1Context>(options => options.UseMySql(builder.Configuration.GetConnectionString("Library1:MariaDB"), ServerVersion.Parse(builder.Configuration.GetSection("DBVersions").GetValue<string>("MariaDB"))));

//builder.Services.AddHttpClient("PrimaryDBConnection")
//    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(5)));

builder.Services.AddTransient<ILibraryRepository, LibraryRepository>();

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
