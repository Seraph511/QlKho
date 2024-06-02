namespace Nhom15_QLKho.Models
{
    public class BaoDuongKho
    {
        public int Id { get; set; }
        public string TenDonVi { get; set; }
        public DateTime NgayBaoDuong { get; set; }
        public string GhiChu { get; set; }

        public List<Kho>? Kho { get; set; }
    }
}
