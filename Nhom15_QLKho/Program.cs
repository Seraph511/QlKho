using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Data;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IHangHoa, EFHangHoa>();
builder.Services.AddScoped<ILoaiHangHoa, EFLoaiHangHoa>();
builder.Services.AddScoped<INhaCungCap, EFNhaCungCap>();
builder.Services.AddScoped<IKho, EFKho>();
builder.Services.AddScoped<IPhieuXuatKho, EFPhieuXuatKho>();
builder.Services.AddScoped<IBaoDuongKho, EFBaoDuongKho>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for hangHoaion scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



//builder.Services.AddScoped<IPhieuXuatKhoRepository, EFPhieuXuatKhoRepository>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Kho}/{action=Index}/{id?}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
