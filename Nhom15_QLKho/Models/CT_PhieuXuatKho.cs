namespace Nhom15_QLKho.Models
{
    public class CT_PhieuXuatKho
    {
        public int Id { get; set; }
        public int PhieuXuatKhoId { get; set; }
        public int HangHoaId { get; set; }
        public int SoLuongXuat { get; set; }
        public float TongGiaTri { get; set; }

        public PhieuXuatKho PhieuXuatKho { get; set; }
        public HangHoa HangHoa { get; set; }
    }
}
