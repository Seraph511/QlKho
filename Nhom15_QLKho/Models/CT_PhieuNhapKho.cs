namespace Nhom15_QLKho.Models
{
    public class CT_PhieuNhapKho
    {
        public int Id { get; set; }
        public int PhieuNhapKhoId { get; set; }
        public int HangHoaId { get; set; }
        public int SoLuongNhap { get; set; }
        public float TongGiaTri { get; set; }

        public PhieuNhapKho PhieuNhapKho { get; set; }
        public HangHoa HangHoa { get; set; }
    }
}
