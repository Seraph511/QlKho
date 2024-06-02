using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
	public interface INhaCungCap
	{
		Task<IEnumerable<NhaCungCap>> GetAllAsync();
		Task<NhaCungCap> GetByIdAsync(int id);
		Task AddAsync(NhaCungCap nhaCungCap);
		Task UpdateAsync(NhaCungCap nhaCungCap);
		Task DeleteAsync(int id);

		IQueryable<NhaCungCap> GetAll();
	}
}
