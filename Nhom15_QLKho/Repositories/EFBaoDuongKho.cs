using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Data;
using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
    public class EFBaoDuongKho : IBaoDuongKho
    {
        private readonly ApplicationDbContext _context;
        public EFBaoDuongKho(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BaoDuongKho>> GetAllAsync()
        {
            // return await _context.HangHoas.ToListAsync();
            return await _context.baoDuongKhos
            .ToListAsync();
        }
        public async Task<BaoDuongKho> GetByIdAsync(int id)
        {
            // return await _context.HangHoas.FindAsync(id);
            // lấy thông tin kèm theo loaiHangHoa
            return await _context.baoDuongKhos.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(BaoDuongKho baoDuongKho)
        {
            _context.baoDuongKhos.Add(baoDuongKho);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(BaoDuongKho baoDuongKho)
        {
            _context.baoDuongKhos.Update(baoDuongKho);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var baoDuongKho = await _context.baoDuongKhos.FindAsync(id);
            _context.baoDuongKhos.Remove(baoDuongKho);
            await _context.SaveChangesAsync();
        }

        public IQueryable<BaoDuongKho> GetAll()
        {
            return _context.baoDuongKhos;
        }
    }
}
