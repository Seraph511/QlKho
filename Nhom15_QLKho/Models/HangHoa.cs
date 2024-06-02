namespace Nhom15_QLKho.Models
{
    public class HangHoa
    {
        public int Id { get; set; }
        public string TenHH { get; set; }
        public float GiaTri { get; set; }
        public string ViTriHang { get; set; }
        public int SoLuongTon { get; set; }
        public int NhaCungCapId { get; set; }
        public NhaCungCap? NhaCungCap { get; set; }

        public int LoaiHangHoaId { get; set; }
        public LoaiHangHoa? LoaiHangHoa { get; set; }

        public int KhoId { get; set; }
        public Kho? Kho { get; set; }
        public string? ImageUrl { get; set; } // Đường dẫn đến hình ảnh đại diện
        public List<HangHoaImages>? Images { get; set; }


    }
}
