using Microsoft.AspNetCore.Mvc;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Controllers
{
    public class KhoController : Controller
    {
        private readonly IKho _khoRepository;

        public KhoController(IKho khoRepository)
        {
            _khoRepository = khoRepository;
        }

        // Hiển thị form thêm sản phẩm mới




    }
}
