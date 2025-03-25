using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using stay_link.Server.Auth;
using stay_link.Server.Auth.Model;
using stay_link.Server.Data;
using System.Text;
using DotNetEnv;
using stay_link.Server.Services;
using stay_link.Server.Mappings;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

Env.Load();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", corsBuilder =>
    {
        corsBuilder.WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_URL")) 
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


builder.Services.AddDbContext<BookingContext>(options =>
    options.UseLazyLoadingProxies()
        .UseNpgsql(Environment.GetEnvironmentVariable("Default_Connection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentity<BookingUser, IdentityRole>()
    .AddEntityFrameworkStores<BookingContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.MapInboundClaims = false;
    configureOptions.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
    configureOptions.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
    configureOptions.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));

    configureOptions.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("AccessToken"))
            {
                context.Token = context.Request.Cookies["AccessToken"];
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.AddHostedService<RoomUsageBackgroundService>();

builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddTransient<SessionService>();
builder.Services.AddScoped<AuthSeeder>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<RoomService>();



var app = builder.Build();

using var scope = app.Services.CreateScope();

SeedData.Initialize(scope.ServiceProvider);
//var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthSeeder>();
//await dbSeeder.SeedAsync();

app.UseCors("AllowFrontend");

app.AddAuthApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
