using Nhom15_QLKho.Models;
namespace Nhom15_QLKho.Repositories
{
    public interface IPhieuXuatKho
    {
        Task<IEnumerable<PhieuXuatKho>> GetAllAsync();
        Task<PhieuXuatKho> GetByIdAsync(int id);
        Task AddAsync(PhieuXuatKho loaiHangHoa);
        Task UpdateAsync(PhieuXuatKho loaiHangHoa);
        Task DeleteAsync(int id);
        IEnumerable<CT_PhieuXuatKho> GetCT_PhieuXuatKhosByPhieuXuatKhoId(int phieuxuatkhoId);
    }
}
