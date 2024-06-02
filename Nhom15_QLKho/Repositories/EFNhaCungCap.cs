using Nhom15_QLKho.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Data;

namespace Nhom15_QLKho.Repositories
{
	public class EFNhaCungCap : INhaCungCap
	{
		private readonly ApplicationDbContext _context;
		public EFNhaCungCap(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<NhaCungCap>> GetAllAsync()
		{
			// return await _context.HangHoas.ToListAsync();
			return await _context.nhaCungCaps.ToListAsync();
		}
		public async Task<NhaCungCap> GetByIdAsync(int id)
		{
			// return await _context.HangHoas.FindAsync(id);
			// lấy thông tin kèm theo loaiHangHoa
			return await _context.nhaCungCaps.FirstOrDefaultAsync(p => p.Id == id);
		}
		public async Task AddAsync(NhaCungCap nhaCungCap)
		{
			_context.nhaCungCaps.Add(nhaCungCap);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(NhaCungCap nhaCungCap)
		{
			_context.nhaCungCaps.Update(nhaCungCap);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var nhaCungCap = await _context.nhaCungCaps.FindAsync(id);
			_context.nhaCungCaps.Remove(nhaCungCap);
			await _context.SaveChangesAsync();
		}

		public IQueryable<NhaCungCap> GetAll()
		{
			return _context.nhaCungCaps;
		}
	}
}
