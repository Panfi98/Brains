using System.Text;
using System.Text.Json.Serialization;
using BrainsToDo.Data;
using BrainsToDo.Repositories;
using BrainsToDo.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BrainsToDo.Interfaces;
using BrainsToDo.Services;
using BrainsToDo.Services.jobFetcher;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(builder.Configuration["AllowedOrigins"]??"http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Worker API", Version = "v1" });
    
    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddMvc();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<ResumeTemplateRepository>();
builder.Services.AddScoped<ResumeRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserSeeder>(); // Register UserSeeder

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("ThisIsYourSecretKeyMakeItAtLeast32CharactersLong"))
        };
    });

builder.Services.AddScoped<ITokenGeneration, TokenGenerationService>();

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddAutoMapper(typeof(ResumeTemplateRepository)); 
builder.Services.AddAutoMapper(typeof(ResumeRepository));

builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Register a typed HttpClient for NavJobClient *before* building the app
builder.Services.AddHttpClient<NavJobClient>();

var app = builder.Build();

if (args.Contains("--seedUsers"))
{
    Console.WriteLine("Detected --seedUsers argument. Attempting to seed users...");
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var userSeeder = services.GetRequiredService<UserSeeder>();
            await userSeeder.SeedUsersAsync();
            Console.WriteLine("User seeding process completed based on --seedUsers argument.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database with users due to --seedUsers argument.");
        }
    } 
    Environment.Exit(0);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();