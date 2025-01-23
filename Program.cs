using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication3;
using AutoMapper;
using WebApplication3.Models;
//using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using WebApplication3.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API của tôi",
        Version = "v1"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.LoginPath = "/login";
    });



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Địa chỉ của ứng dụng React trong phát triển
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

//builder.Services.AddSpaStaticFiles(configuration =>
//{
//    configuration.RootPath = "ClientApp/build"; // Đường dẫn tới thư mục build của React
//});

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API của tôi v1");
        c.RoutePrefix = string.Empty; // Đặt thành trống để Swagger UI nằm ở trang gốc
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//app.UseSpaStaticFiles();
//app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
//{
//    builder.UseSpa(spa =>
//    {
//        spa.Options.SourcePath = "ClientApp"; // Đường dẫn tới thư mục chứa mã nguồn React

//        if (app.Environment.IsDevelopment())
//        {
//            // Nếu môi trường phát triển, sử dụng React development server
//            spa.UseReactDevelopmentServer(npmScript: "start");
//        }
//    });
//});

app.Run();

app.UseSwagger();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDefaultFiles();  // Đảm bảo trang mặc định được phục vụ


app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

