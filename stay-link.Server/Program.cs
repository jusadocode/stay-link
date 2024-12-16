using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using stay_link.Server.Auth;
using stay_link.Server.Auth.Model;
using stay_link.Server.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://localhost:5173") 
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


builder.Services.AddDbContext<BookingContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    configureOptions.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));

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

builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddTransient<SessionService>();
builder.Services.AddScoped<AuthSeeder>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

//var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthSeeder>();
await dbSeeder.SeedAsync();

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
