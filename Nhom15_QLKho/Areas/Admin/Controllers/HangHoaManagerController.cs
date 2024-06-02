using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class HangHoaManagerController : Controller
	{
		private readonly IHangHoa _hangHoaRepository;
		private readonly ILoaiHangHoa _loaiHangHoaRepository;
		private readonly INhaCungCap _nhaCungCapRepository;
		private readonly IKho _khoRepository;
		
		public HangHoaManagerController(IHangHoa hangHoaRepository,
		ILoaiHangHoa loaiHangHoaRepository, INhaCungCap nhaCungCapRepository, IKho khoRepository)
		{
			_hangHoaRepository = hangHoaRepository;
			_loaiHangHoaRepository = loaiHangHoaRepository;
			_nhaCungCapRepository = nhaCungCapRepository;
            _khoRepository = khoRepository;


        }
		// Hiển thị danh sách sản phẩm
		public async Task<IActionResult> Index(string searchString)
		{
			var hangHoas = _hangHoaRepository.GetAll();

			if (!String.IsNullOrEmpty(searchString))
			{
				hangHoas = hangHoas.Where(s => s.TenHH.Contains(searchString));
			}

			return View(await hangHoas.ToListAsync());
		}


		// Hiển thị form thêm sản phẩm mới
		public async Task<IActionResult> Add()
		{
			var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
			var nhaCungCaps = await _nhaCungCapRepository.GetAllAsync();
			var Khos = await _khoRepository.GetAllAsync();
			

			ViewBag.loaiHangHoas = new SelectList(loaiHangHoas, "Id", "TenLoai");
			ViewBag.nhaCungCaps = new SelectList(nhaCungCaps, "Id", "TenNCC");
			ViewBag.Khos = new SelectList(Khos, "Id", "TenKho");
			
			return View();
		}
		// Xử lý thêm sản phẩm mới
		[HttpPost]
		public async Task<IActionResult> Add(HangHoa hangHoa, IFormFile
		imageUrl)
		{
			if (ModelState.IsValid)
			{
				if (imageUrl != null)
				{
					// Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage
					hangHoa.ImageUrl = await SaveImage(imageUrl);
				}
				await _hangHoaRepository.AddAsync(hangHoa);
				return RedirectToAction(nameof(Index));
			}
			// Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
			var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
			ViewBag.loaiHangHoas = new SelectList(loaiHangHoas, "Id", "Name");

			var nhaCungCaps = await _nhaCungCapRepository.GetAllAsync();
			ViewBag.nhaCungCaps = new SelectList(nhaCungCaps, "Id", "Name");

            var Khos = await _khoRepository.GetAllAsync();
            ViewBag.Khos = new SelectList(Khos, "Id", "Name");
            return View(hangHoa);
		}

		// Viết thêm hàm SaveImage (tham khảo bài 02)
		private async Task<string> SaveImage(IFormFile image)
		{
			var savePath = Path.Combine("wwwroot/images", image.FileName); //

			using (var fileStream = new FileStream(savePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}
			return "/images/" + image.FileName; // Trả về đường dẫn tương đối
		}
		//Nhớ tạo folder images trong wwwroot
		// Hiển thị thông tin chi tiết sản phẩm
		public async Task<IActionResult> Display(int id)
		{
			var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
			if (hangHoa == null)
			{
				return NotFound();
			}
			return View(hangHoa);
		}


		// Hiển thị form cập nhật sản phẩm
		public async Task<IActionResult> Update(int id)
		{
			var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
			if (hangHoa == null)
			{
				return NotFound();
			}
			var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
			var nhaCungCaps = await _nhaCungCapRepository.GetAllAsync();
            var Khos = await _khoRepository.GetAllAsync();

            ViewBag.loaiHangHoas = new SelectList(loaiHangHoas, "Id", "Name", hangHoa.LoaiHangHoaId);
			ViewBag.nhaCungCaps = new SelectList(nhaCungCaps, "Id", "Name", hangHoa.NhaCungCapId);
			ViewBag.Khos = new SelectList(Khos, "Id", "Name", hangHoa.KhoId);

            return View(hangHoa);
		}
		// Xử lý cập nhật sản phẩm
		[HttpPost]
		public async Task<IActionResult> Update(int id, HangHoa hangHoa,
		IFormFile imageUrl)
		{
			ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho

			if (id != hangHoa.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				var existingHangHoa = await
				_hangHoaRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync

				if (imageUrl == null)
				{
					hangHoa.ImageUrl = existingHangHoa.ImageUrl;
				}
				else
				{
					// Lưu hình ảnh mới
					hangHoa.ImageUrl = await SaveImage(imageUrl);
				}
				// Cập nhật các thông tin khác của sản phẩm

				existingHangHoa.TenHH = hangHoa.TenHH;
				existingHangHoa.GiaTri = hangHoa.GiaTri;
                existingHangHoa.ViTriHang = hangHoa.ViTriHang;
                existingHangHoa.SoLuongTon = hangHoa.SoLuongTon;
                existingHangHoa.LoaiHangHoaId = hangHoa.LoaiHangHoaId;
				existingHangHoa.NhaCungCapId = hangHoa.NhaCungCapId;
				existingHangHoa.KhoId = hangHoa.KhoId;
				
				
				existingHangHoa.ImageUrl = hangHoa.ImageUrl;
				await _hangHoaRepository.UpdateAsync(existingHangHoa);
				return RedirectToAction(nameof(Index));
			}
			var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
			ViewBag.loaiHangHoas = new SelectList(loaiHangHoas, "Id", "Name");
			return View(hangHoa);
		}
		// Hiển thị form xác nhận xóa sản phẩm
		public async Task<IActionResult> Delete(int id)
		{
			var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
			if (hangHoa == null)
			{
				return NotFound();
			}
			return View(hangHoa);
		}
		// Xử lý xóa sản phẩm
		[HttpPost, ActionName("DeleteConfirmed")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _hangHoaRepository.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
