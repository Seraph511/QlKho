using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
	public interface IHangHoa
	{
		Task<IEnumerable<HangHoa>> GetAllAsync();
		Task<HangHoa> GetByIdAsync(int id);
		Task AddAsync(HangHoa hangHoa);
		Task UpdateAsync(HangHoa hangHoa);
		Task DeleteAsync(int id);

		IQueryable<HangHoa> GetAll();
	}
}
