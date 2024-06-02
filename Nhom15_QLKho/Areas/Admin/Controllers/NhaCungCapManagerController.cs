using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class NhaCungCapManagerController : Controller
	{
		private readonly INhaCungCap _nhaCungCapRepository;

		public NhaCungCapManagerController(INhaCungCap nhaCungCapRepository)
		{
			_nhaCungCapRepository = nhaCungCapRepository;
		}

		public async Task<IActionResult> Index(string searchString)
		{
			var nhaCungCaps = _nhaCungCapRepository.GetAll();

			if (!String.IsNullOrEmpty(searchString))
			{
                nhaCungCaps = nhaCungCaps.Where(s => s.TenNCC.Contains(searchString));
			}

			return View(await nhaCungCaps.ToListAsync());
		}

		public async Task<IActionResult> Add()
		{
			return View();
		}
		// Xử lý thêm sản phẩm mới
		[HttpPost]
		public async Task<IActionResult> Add(NhaCungCap nhaCungCap)
		{
			if (nhaCungCap == null)
			{
				return BadRequest("LoaiHangHoa object is null");
			}

			if (ModelState.IsValid)
			{
				await _nhaCungCapRepository.AddAsync(nhaCungCap);
				return RedirectToAction(nameof(Index));
			}
			return View(nhaCungCap);
		}

		public async Task<IActionResult> Update(int id)
		{
			var nhaCungCap = await _nhaCungCapRepository.GetByIdAsync(id);
			if (nhaCungCap == null)
			{
				return NotFound();
			}

			return View(nhaCungCap);
		}
		// Xử lý cập nhật sản phẩm

		[HttpPost]
		public async Task<IActionResult> Update(int id, NhaCungCap nhaCungCap)
		{
			if (ModelState.IsValid)
			{
				var existingNhaCungCap = await _nhaCungCapRepository.GetByIdAsync(id);
				if (existingNhaCungCap == null)
				{
					return NotFound();
				}

				// Cập nhật các thông tin khác của danh mục
				existingNhaCungCap.TenNCC = nhaCungCap.TenNCC;
				existingNhaCungCap.DiaChi = nhaCungCap.DiaChi;
				existingNhaCungCap.SDT = nhaCungCap.SDT;

				await _nhaCungCapRepository.UpdateAsync(existingNhaCungCap);

				return RedirectToAction(nameof(Index));
			}
			return View(nhaCungCap);
		}


		// Hiển thị form xác nhận xóa sản phẩm
		public async Task<IActionResult> Delete(int id)
		{
			var nhaCungCap = await _nhaCungCapRepository.GetByIdAsync(id);
			if (nhaCungCap == null)
			{
				return NotFound();
			}
			return View(nhaCungCap);
		}

		// Xử lý xóa sản phẩm
		[HttpPost, ActionName("DeleteConfirmed")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _nhaCungCapRepository.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Display(int id)
		{
			var hangHoa = await _nhaCungCapRepository.GetByIdAsync(id);
			if (hangHoa == null)
			{
				return NotFound();
			}
			return View(hangHoa);
		}
	}
}
