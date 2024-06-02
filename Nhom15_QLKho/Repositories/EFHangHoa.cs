using Nhom15_QLKho.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Data;
namespace Nhom15_QLKho.Repositories
{
	public class EFHangHoa : IHangHoa
	{
		private readonly ApplicationDbContext _context;
		public EFHangHoa(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<HangHoa>> GetAllAsync()
		{
			// return await _context.HangHoas.ToListAsync();
			return await _context.HangHoas
			.Include(p => p.LoaiHangHoa) // Include thông tin về loaiHangHoa
			.Include(p => p.NhaCungCap)
			.Include(p => p.Kho)
			.ToListAsync();
		}
		public async Task<HangHoa> GetByIdAsync(int id)
		{
			// return await _context.HangHoas.FindAsync(id);
			// lấy thông tin kèm theo loaiHangHoa
			return await _context.HangHoas
			.Include(p => p.NhaCungCap)
			.Include(p => p.LoaiHangHoa)
            .Include(p => p.Kho)

            .FirstOrDefaultAsync(p => p.Id == id);
		}
		public async Task AddAsync(HangHoa hangHoa)
		{
			_context.HangHoas.Add(hangHoa);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(HangHoa hangHoa)
		{
			_context.HangHoas.Update(hangHoa);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var hangHoa = await _context.HangHoas.FindAsync(id);
			_context.HangHoas.Remove(hangHoa);
			await _context.SaveChangesAsync();
		}

		public IQueryable<HangHoa> GetAll()
		{
			return _context.HangHoas
				.Include(p => p.LoaiHangHoa)
				.Include(p => p.NhaCungCap)
				.Include(p => p.Kho);


        }
    }
}
