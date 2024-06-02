using Nhom15_QLKho.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Data;

namespace Nhom15_QLKho.Repositories
{
	public class EFLoaiHangHoa : ILoaiHangHoa
	{
		private readonly ApplicationDbContext _context;
		public EFLoaiHangHoa(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<LoaiHangHoa>> GetAllAsync()
		{
			// return await _context.HangHoas.ToListAsync();
			return await _context.loaiHangHoas
			.ToListAsync();
		}
		public async Task<LoaiHangHoa> GetByIdAsync(int id)
		{
			// return await _context.HangHoas.FindAsync(id);
			// lấy thông tin kèm theo loaiHangHoa
			return await _context.loaiHangHoas.FirstOrDefaultAsync(p => p.Id == id);
		}
		public async Task AddAsync(LoaiHangHoa loaiHangHoa)
		{
			_context.loaiHangHoas.Add(loaiHangHoa);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(LoaiHangHoa loaiHangHoa)
		{
			_context.loaiHangHoas.Update(loaiHangHoa);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var loaiHangHoa = await _context.loaiHangHoas.FindAsync(id);
			_context.loaiHangHoas.Remove(loaiHangHoa);
			await _context.SaveChangesAsync();
		}

		public IQueryable<LoaiHangHoa> GetAll()
		{
			return _context.loaiHangHoas;
		}
	}
}
