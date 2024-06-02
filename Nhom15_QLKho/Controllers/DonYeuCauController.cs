using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nhom15_QLKho.Data;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;


namespace Nhom15_QLKho.Controllers
{
	public class DonYeuCauController : Controller
	{
		private readonly IHangHoa _hangHoaRepository;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public DonYeuCauController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHangHoa hangHoaRepository)
		{
			_hangHoaRepository = hangHoaRepository;
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> AddToDon(int hangHoaId, int soLuongTon)
		{
			// Giả sử bạn có phương thức lấy thông tin sản phẩm từ hangHoaId
			var hangHoa = await GetHangHoaFromDatabase(hangHoaId);
			if (hangHoa == null)
			{
				// Xử lý trường hợp hàng hóa không tồn tại
				return NotFound();
			}

			var donYeuCau = new DonYeuCau
			{
				HangHoaId = hangHoaId,
				SoLuong = soLuongTon
			};

			//var don = HttpContext.Session.GetObjectFromJson<CT_DonYeuCau>("Don") ?? new CT_DonYeuCau();
			//don.AddItem(donYeuCau);
			//HttpContext.Session.SetObjectAsJson("Don", don);
			return RedirectToAction("Index");
		}

		//public IActionResult Index()
		//{
			//var don = HttpContext.Session.GetObjectFromJson<CT_DonYeuCau>("Don") ?? new CT_DonYeuCau();
			//return View(don);
		//}

		//public async Task<IActionResult> RemoveFromDon(int hangHoaId)
		//{
			//var don = HttpContext.Session.GetObjectFromJson<CT_DonYeuCau>("Don");
			//if (don != null)
			//{
			//	don.RemoveItem(hangHoaId);
				// Lưu lại đơn yêu cầu vào Session sau khi đã xóa mục
				//HttpContext.Session.SetObjectAsJson("Don", don);
			//}
			//return RedirectToAction("Index");
		//}

		public IActionResult Checkout()
		{
			return View(new PhieuXuatKho());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Checkout(PhieuXuatKho phieuXuatKho)
		{
			//var don = HttpContext.Session.GetObjectFromJson<CT_DonYeuCau>("Don");
			//if (don == null || !don.Items.Any())
			//{
				// Xử lý giỏ hàng trống...
				return RedirectToAction("Index");
			//}

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				// Xử lý trường hợp người dùng không tồn tại hoặc không đăng nhập
				return Unauthorized();
			}

			phieuXuatKho.UserId = user.Id;
			phieuXuatKho.NgayXuat = DateTime.UtcNow;
			//phieuXuatKho.TongGiaTri = don.Items.Sum(i => i.GiaTri * i.SoLuong);
			//phieuXuatKho.CT_PhieuXuatKhos = don.Items.Select(i => new CT_PhieuXuatKho
			//{
			//	HangHoaId = i.HangHoaId,
			//	SoLuongXuat = i.SoLuong,
			//	TongGiaTri = i.GiaTri * i.SoLuong
			//}).ToList();

			_context.PhieuXuatKhos.Add(phieuXuatKho);
			await _context.SaveChangesAsync();

			HttpContext.Session.Remove("Don");
			return View("PhieuXuatKhoCompleted", phieuXuatKho); // Trang xác nhận hoàn thành đơn hàng
		}

		private async Task<HangHoa> GetHangHoaFromDatabase(int hangHoaId)
		{
			// Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm
			return await _hangHoaRepository.GetByIdAsync(hangHoaId);
		}
	}
}
