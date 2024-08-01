using ContactsApi.Data;
using ContactsApi.Repositories;
using ContactsApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the DbContext
builder.Services.AddDbContext<ContactContext>(opt => opt.UseInMemoryDatabase("ContactList"));

// Add the repositories and services
builder.Services.AddScoped<ContactRepository>();
builder.Services.AddScoped<ContactService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ContactsApi.Middleware.ErrorHandlingMiddleware>(); // Global error handling middleware

app.UseHttpsRedirection();

// Use the specific CORS policy defined above
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
