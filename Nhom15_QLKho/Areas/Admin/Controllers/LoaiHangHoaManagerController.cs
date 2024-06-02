using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Areas.Admin.Controllers
{

	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class LoaiHangHoaManagerController : Controller
	{
		private readonly ILoaiHangHoa _loaiHangHoaRepository;

		public LoaiHangHoaManagerController(ILoaiHangHoa loaiHangHoaRepository)
		{
			_loaiHangHoaRepository = loaiHangHoaRepository;
		}

		// Hiển thị danh sách sản phẩm
		public async Task<IActionResult> Index(string searchString)
		{
			var loaiHangHoas = _loaiHangHoaRepository.GetAll();

			if (!String.IsNullOrEmpty(searchString))
			{
				loaiHangHoas = loaiHangHoas.Where(s => s.TenLoai.Contains(searchString));
			}

			return View(await loaiHangHoas.ToListAsync());
		}
		// Hiển thị form thêm sản phẩm mới

		public async Task<IActionResult> Add()
		{
			return View();
		}
		// Xử lý thêm sản phẩm mới
		[HttpPost]
		public async Task<IActionResult> Add(LoaiHangHoa loaiHangHoa)
		{
			if (loaiHangHoa == null)
			{
				return BadRequest("LoaiHangHoa object is null");
			}

			if (ModelState.IsValid)
			{
				await _loaiHangHoaRepository.AddAsync(loaiHangHoa);
				return RedirectToAction(nameof(Index));
			}
			return View(loaiHangHoa);
		}


		// Hiển thị form cập nhật sản phẩm

		public async Task<IActionResult> Update(int id)
		{
			var loaiHangHoa = await _loaiHangHoaRepository.GetByIdAsync(id);
			if (loaiHangHoa == null)
			{
				return NotFound();
			}

			return View(loaiHangHoa);
		}
		// Xử lý cập nhật sản phẩm
		[HttpPost]
		public async Task<IActionResult> Update(int id, LoaiHangHoa loaiHangHoa)
		{
			if (ModelState.IsValid)
			{
				var existingLoaiHangHoa = await _loaiHangHoaRepository.GetByIdAsync(id);
				if (existingLoaiHangHoa == null)
				{
					return NotFound();
				}

				// Cập nhật các thông tin khác của danh mục
				existingLoaiHangHoa.TenLoai = loaiHangHoa.TenLoai;
			

				await _loaiHangHoaRepository.UpdateAsync(existingLoaiHangHoa);

				return RedirectToAction(nameof(Index));
			}
			return View(loaiHangHoa);
		}


		// Hiển thị form xác nhận xóa sản phẩm
		public async Task<IActionResult> Delete(int id)
		{
			var loaiHangHoa = await _loaiHangHoaRepository.GetByIdAsync(id);
			if (loaiHangHoa == null)
			{
				return NotFound();
			}
			return View(loaiHangHoa);
		}

		// Xử lý xóa sản phẩm
		[HttpPost, ActionName("DeleteConfirmed")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _loaiHangHoaRepository.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Display(int id)
		{
			var hangHoa = await _loaiHangHoaRepository.GetByIdAsync(id);
			if (hangHoa == null)
			{
				return NotFound();
			}
			return View(hangHoa);
		}
	}
}
