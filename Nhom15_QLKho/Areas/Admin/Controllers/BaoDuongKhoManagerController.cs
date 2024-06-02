using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BaoDuongKhoManagerController : Controller
    {
        private readonly IBaoDuongKho _baoDuongKhoRepository;

        public BaoDuongKhoManagerController(IBaoDuongKho baoDuongKhoRepository)
        {
            _baoDuongKhoRepository = baoDuongKhoRepository;
        }

        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index(string searchString)
        {
            var baoDuongKhos = _baoDuongKhoRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                baoDuongKhos = baoDuongKhos.Where(s => s.TenDonVi.Contains(searchString));
            }

            return View(await baoDuongKhos.ToListAsync());
        }
        // Hiển thị form thêm sản phẩm mới

        public async Task<IActionResult> Add()
        {
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(BaoDuongKho baoDuongKho)
        {
            if (baoDuongKho == null)
            {
                return BadRequest("BaoDuongKho object is null");
            }

            if (ModelState.IsValid)
            {
                await _baoDuongKhoRepository.AddAsync(baoDuongKho);
                return RedirectToAction(nameof(Index));
            }
            return View(baoDuongKho);
        }


        // Hiển thị form cập nhật sản phẩm

        public async Task<IActionResult> Update(int id)
        {
            var baoDuongKho = await _baoDuongKhoRepository.GetByIdAsync(id);
            if (baoDuongKho == null)
            {
                return NotFound();
            }

            return View(baoDuongKho);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, BaoDuongKho baoDuongKho)
        {
            if (ModelState.IsValid)
            {
                var existingBaoDuongKho = await _baoDuongKhoRepository.GetByIdAsync(id);
                if (existingBaoDuongKho == null)
                {
                    return NotFound();
                }

                // Cập nhật các thông tin khác của danh mục
                existingBaoDuongKho.TenDonVi = baoDuongKho.TenDonVi;
                existingBaoDuongKho.NgayBaoDuong = baoDuongKho.NgayBaoDuong;
                existingBaoDuongKho.GhiChu = baoDuongKho.GhiChu;

                await _baoDuongKhoRepository.UpdateAsync(existingBaoDuongKho);

                return RedirectToAction(nameof(Index));
            }
            return View(baoDuongKho);
        }


        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var baoDuongKho = await _baoDuongKhoRepository.GetByIdAsync(id);
            if (baoDuongKho == null)
            {
                return NotFound();
            }
            return View(baoDuongKho);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _baoDuongKhoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int id)
        {
            var hangHoa = await _baoDuongKhoRepository.GetByIdAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return View(hangHoa);
        }
    }
}
