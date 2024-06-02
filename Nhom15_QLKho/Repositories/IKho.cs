using Nhom15_QLKho.Models;

namespace Nhom15_QLKho.Repositories
{
    public interface IKho
    {
        Task<IEnumerable<Kho>> GetAllAsync();
        Task<Kho> GetByIdAsync(int id);
        Task AddAsync(Kho kho);
        Task UpdateAsync(Kho kho);
        Task DeleteAsync(int id);

        IQueryable<Kho> GetAll();
    }
}
