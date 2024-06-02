using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom15_QLKho.Models
{
    public class PhieuNhapKho
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime NgayNhap { get; set; }
        public string Notes { get; set; }
      
        [ForeignKey("User")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public List<CT_PhieuNhapKho> CT_PhieuNhapKhos { get; set; }
    }
}
