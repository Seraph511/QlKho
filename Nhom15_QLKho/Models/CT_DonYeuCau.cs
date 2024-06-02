namespace Nhom15_QLKho.Models
{
    public class CT_DonYeuCau
    {
        public List<DonYeuCau> Items { get; set; } = new List<DonYeuCau>();
        public void AddItem(DonYeuCau item)
        {
            var existingItem = Items.FirstOrDefault(i => i.HangHoaId ==
            item.HangHoaId);
            if (existingItem != null)
            {
                existingItem.SoLuong += item.SoLuong;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int HangHoaId)
        {
            Items.RemoveAll(i => i.HangHoaId == HangHoaId);
        }
        // Các phương thức khác...
    }
}
