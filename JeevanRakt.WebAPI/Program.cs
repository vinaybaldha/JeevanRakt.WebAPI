using JeevanRakt.Core.Domain.Identity;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Core.ServiceContracts;
using JeevanRakt.Core.Services;
using JeevanRakt.Infrastructure.DataBase;
using JeevanRakt.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Stripe;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //Authorization policy
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});
//add swagger service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add connectionString
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//add Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
    .AddRoles<ApplicationRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("HotelBooling")
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>()
    .AddDefaultTokenProviders();

//add services
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
builder.Services.AddScoped<IBloodBankService, BloodBankService>();
builder.Services.AddTransient<DataGeneraterService>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IRecipientRepository, RecipientRepository>();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();


//add cors
builder.Services.AddCors(option => option.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200");
}));

builder.Services.AddSignalR();
builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
//add authentication service
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//add authorization
        builder.Services.AddAuthorization(options =>
        {

        });

//add httpContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();
    

    
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseStaticFiles(
        new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
            RequestPath = "/Images"
        }
    );

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.MapHub<ProductNotificationHub>("/Notify");

app.Run();