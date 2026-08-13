using hospital_api.Data;
using hospital_api.Interface.Repository;
using hospital_api.Interface.Services;
using hospital_api.Models;
using hospital_api.Repositories;
using hospital_api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        builder => builder.WithOrigins("http://localhost:7245", "http://localhost:5203")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


//Service 
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IReferenceData, ReferenceDataService>();
builder.Services.AddScoped<IReferenceDataCategory, ReferenceDataCategoryService>();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();


//Command
builder.Services.AddTransient<ICommandRepository<Appointment>, CommandRepository<Appointment>>();
builder.Services.AddTransient<ICommandRepository<Doctor>, CommandRepository<Doctor>>();
builder.Services.AddTransient<ICommandRepository<Patient>, CommandRepository<Patient>>();
builder.Services.AddTransient<ICommandRepository<ReferenceData>, CommandRepository<ReferenceData>>();
builder.Services.AddTransient<ICommandRepository<ReferenceDataCategory>, CommandRepository<ReferenceDataCategory>>();



//Query
builder.Services.AddTransient<IQueryRepository<Appointment>, AppointmentRepository>();
builder.Services.AddTransient<IQueryRepository<Doctor>, DoctorRepository>();
builder.Services.AddTransient<IQueryRepository<Patient>, PatientRepository>();
builder.Services.AddTransient<IQueryRepository<ReferenceData>, ReferenceDataRepository>();
builder.Services.AddTransient<IQueryRepository<ReferenceDataCategory>, ReferenceDataCategoryRepository>();

builder.Services.AddAutoMapper(typeof(Program)); // Add this line
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlify", policy =>
    {
        policy.WithOrigins("https://hospital-websystem.netlify.app")
              .AllowAnyHeader()
              .AllowAnyMethod();
              // .AllowCredentials(); // only add this if you're sending cookies/auth headers that need it
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowNetlify");
app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 

app.Run();
