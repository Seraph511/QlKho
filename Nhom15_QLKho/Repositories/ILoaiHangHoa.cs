using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
	public interface ILoaiHangHoa
	{
		Task<IEnumerable<LoaiHangHoa>> GetAllAsync();
		Task<LoaiHangHoa> GetByIdAsync(int id);
		Task AddAsync(LoaiHangHoa loaiHangHoa);
		Task UpdateAsync(LoaiHangHoa loaiHangHoa);
		Task DeleteAsync(int id);

		IQueryable<LoaiHangHoa> GetAll();
	}
}
