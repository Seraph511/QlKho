using Microsoft.AspNetCore.Mvc;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Controllers
{
	public class NhaCungCapController : Controller
	{
		private readonly INhaCungCap _nhaCungCapRepository;

		public NhaCungCapController(INhaCungCap nhaCungCapRepository)
		{
			_nhaCungCapRepository = nhaCungCapRepository;
		}

		// Hiển thị form thêm sản phẩm mới




	}
}
