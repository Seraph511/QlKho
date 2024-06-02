namespace Nhom15_QLKho.Models
{
    public class HangHoaImages
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int HangHoaId { get; set; }
        public HangHoa? HangHoa { get; set; }
    }
}
