using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom15_QLKho.Models
{
    public class Kho
    {
        public int Id { get; set; }
       
        public string TenKho { get; set; }
        public string ViTriKho { get; set; }
        public int SucChua { get; set; }
        public int BaoDuongKhoId {  get; set; }
        public BaoDuongKho? BaoDuongKho { get;set; }
      
    }
}
