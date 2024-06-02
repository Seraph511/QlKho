using Microsoft.AspNetCore.Mvc;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Controllers
{
	public class LoaiHangHoaController : Controller
	{
		private readonly ILoaiHangHoa _loaiHangHoaRepository;

		public LoaiHangHoaController(ILoaiHangHoa loaiHangHoaRepository)
		{
			_loaiHangHoaRepository = loaiHangHoaRepository;
		}

		// Hiển thị danh sách sản phẩm

	}
}
