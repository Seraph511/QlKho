namespace Nhom15_QLKho.Models
{
    public class LoaiHangHoa
    {
        public int Id { get; set; }
       
        public string TenLoai { get; set; }
        public string Note { get; set; }

        public List<HangHoa>? HangHoa { get; set; }
    }
}
