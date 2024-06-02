using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
       options) : base(options)
        {
        }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<BaoDuongKho> baoDuongKhos { get; set; }
        public DbSet<Kho> Khos { get; set; }

        public DbSet<LoaiHangHoa> loaiHangHoas { get; set; }
        public DbSet<NhaCungCap> nhaCungCaps { get; set; }
        public DbSet<PhieuHen> phieuHens { get; set; }
        public DbSet<PhieuNhapKho> PhieuNhapKhos { get; set; }
        public DbSet<PhieuXuatKho> PhieuXuatKhos { get; set; }
        public DbSet<CT_PhieuNhapKho> CT_PhieuNhapKhos { get; set; }
        public DbSet<CT_PhieuXuatKho> CT_PhieuXuatKhos { get; set; }
        public DbSet<HangHoaImages> HangHoaImagess { get; set; }

       
    }
}
