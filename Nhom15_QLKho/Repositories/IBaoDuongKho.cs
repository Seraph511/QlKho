using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
    public interface IBaoDuongKho
    {
        Task<IEnumerable<BaoDuongKho>> GetAllAsync();
        Task<BaoDuongKho> GetByIdAsync(int id);
        Task AddAsync(BaoDuongKho baoDuongKho);
        Task UpdateAsync(BaoDuongKho baoDuongKho);
        Task DeleteAsync(int id);

        IQueryable<BaoDuongKho> GetAll();
    }
}
