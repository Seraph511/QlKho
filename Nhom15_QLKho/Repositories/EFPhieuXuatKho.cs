using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Data;
using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
    public class EFPhieuXuatKho : IPhieuXuatKho
    {
        private readonly ApplicationDbContext _context;
        public EFPhieuXuatKho(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PhieuXuatKho>> GetAllAsync()
        {
            // return await _context.Khos.ToListAsync();
            return await _context.PhieuXuatKhos
                .Include(p => p.ApplicationUser)
            .ToListAsync();
        }
        public async Task<PhieuXuatKho> GetByIdAsync(int id)
        {
            // return await _context.Khos.FindAsync(id);
            // lấy thông tin kèm theo loaiHangHoa
            return await _context.PhieuXuatKhos.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(PhieuXuatKho phieuXuatKho)
        {
            _context.PhieuXuatKhos.Add(phieuXuatKho);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PhieuXuatKho phieuXuatKho)
        {
            _context.PhieuXuatKhos.Update(phieuXuatKho);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var phieuXuatKho = await _context.PhieuXuatKhos.FindAsync(id);
            _context.PhieuXuatKhos.Remove(phieuXuatKho);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<CT_PhieuXuatKho> GetCT_PhieuXuatKhosByPhieuXuatKhoId(int phieuXuatKhoId)
        {
            // Query the database to retrieve phieuXuatKho details for the given phieuXuatKho ID
            return _context.CT_PhieuXuatKhos
                .Include(od => od.HangHoa)
                .Where(od => od.PhieuXuatKhoId == phieuXuatKhoId)
                .ToList();
        }

    }
}
