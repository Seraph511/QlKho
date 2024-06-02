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
    public class KhoManagerController : Controller
    {
        private readonly IKho _khoRepository;
        private readonly IBaoDuongKho _baoDuongKhoRepository;

        public KhoManagerController(IKho khoRepository, IBaoDuongKho baoDuongKhoRepository)
        {
            _khoRepository = khoRepository;
            _baoDuongKhoRepository = baoDuongKhoRepository;
        }

        // Hiển thị danh sách kho
        public async Task<IActionResult> Index(string searchString)
        {
            var khos = _khoRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                khos = khos.Where(s => s.TenKho.Contains(searchString));
            }

            return View(await khos.ToListAsync());
        }

        // Hiển thị form thêm kho mới
        public async Task<IActionResult> Add()
        {
            var baoDuongKhos = await _baoDuongKhoRepository.GetAllAsync();
            ViewBag.BaoDuongKhos = new SelectList(baoDuongKhos, "Id", "TenDonVi");
            return View();
        }

        // Xử lý thêm kho mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Kho kho)
        {
            if (ModelState.IsValid)
            {
                await _khoRepository.AddAsync(kho);
                return RedirectToAction(nameof(Index));
            }

            var baoDuongKhos = await _baoDuongKhoRepository.GetAllAsync();
            ViewBag.BaoDuongKhos = new SelectList(baoDuongKhos, "Id", "TenDonVi");
            return View(kho);
        }

        // Hiển thị thông tin chi tiết kho
        public async Task<IActionResult> Display(int id)
        {
            var kho = await _khoRepository.GetByIdAsync(id);
            if (kho == null)
            {
                return NotFound();
            }
            return View(kho);
        }

        // Hiển thị form cập nhật kho
        public async Task<IActionResult> Update(int id)
        {
            var kho = await _khoRepository.GetByIdAsync(id);
            if (kho == null)
            {
                return NotFound();
            }
            var baoDuongKhos = await _baoDuongKhoRepository.GetAllAsync();
            ViewBag.BaoDuongKhos = new SelectList(baoDuongKhos, "Id", "TenDonVi", kho.BaoDuongKhoId);
            return View(kho);
        }

        // Xử lý cập nhật kho
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Kho kho)
        {
            if (id != kho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingKho = await _khoRepository.GetByIdAsync(id);
                if (existingKho == null)
                {
                    return NotFound();
                }

                existingKho.TenKho = kho.TenKho;
                existingKho.SucChua = kho.SucChua;
                existingKho.ViTriKho = kho.ViTriKho;
                existingKho.BaoDuongKhoId = kho.BaoDuongKhoId;

                await _khoRepository.UpdateAsync(existingKho);
                return RedirectToAction(nameof(Index));
            }

            var baoDuongKhos = await _baoDuongKhoRepository.GetAllAsync();
            ViewBag.BaoDuongKhos = new SelectList(baoDuongKhos, "Id", "TenDonVi", kho.BaoDuongKhoId);
            return View(kho);
        }

        // Hiển thị form xác nhận xóa kho
        public async Task<IActionResult> Delete(int id)
        {
            var kho = await _khoRepository.GetByIdAsync(id);
            if (kho == null)
            {
                return NotFound();
            }
            return View(kho);
        }

        // Xử lý xóa kho
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kho = await _khoRepository.GetByIdAsync(id);
            if (kho == null)
            {
                return NotFound();
            }

            await _khoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
