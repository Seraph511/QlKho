using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Data;
using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
    public class EFKho : IKho
    {
        private readonly ApplicationDbContext _context;
        public EFKho(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Kho>> GetAllAsync()
        {
            // return await _context.HangHoas.ToListAsync();
            return await _context.Khos
            .Include(p => p.BaoDuongKho) 
            

            .ToListAsync();
        }
        public async Task<Kho> GetByIdAsync(int id)
        {
            // return await _context.HangHoas.FindAsync(id);
            // lấy thông tin kèm theo loaiHangHoa
            return await _context.Khos
            .Include(p => p.BaoDuongKho)
            

            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Kho kho)
        {
            _context.Khos.Add(kho);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Kho kho)
        {
            _context.Khos.Update(kho);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var kho = await _context.Khos.FindAsync(id);
            _context.Khos.Remove(kho);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Kho> GetAll()
        {
            return _context.Khos
                .Include(p => p.BaoDuongKho);
               


        }
    }
}
