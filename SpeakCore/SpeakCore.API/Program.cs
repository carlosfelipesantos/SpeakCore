using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SpeakCore.Application.Services.Implementations;
using SpeakCore.Application.Services.Interfaces;
using SpeakCore.Domain.Interfaces;
using SpeakCore.Infrastructure.Data;
using SpeakCore.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.    


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<DateTimeSchemaFilter>();
});

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

public class DateTimeConverter : JsonConverter<DateTime>
{
    private const string DateFormat = "dd-MM-yyyy";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString() ?? throw new JsonException("Data inválida.");
        return DateTime.ParseExact(dateString, DateFormat, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}


public class DateTimeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(DateTime) || context.Type == typeof(DateTime?))
        {
            schema.Example = new Microsoft.OpenApi.Any.OpenApiString("30-03-2026");
            schema.Format = "date"; 
        }
    }
}
