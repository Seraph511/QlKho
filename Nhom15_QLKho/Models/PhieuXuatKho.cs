using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom15_QLKho.Models
{
    public class PhieuXuatKho
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime NgayXuat { get; set; }
        public float TongGiaTri { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        //public int PhieuHenId { get; set; }
        //public PhieuHen PhieuHen { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public List<CT_PhieuXuatKho> CT_PhieuXuatKhos { get; set; }

    }
}
