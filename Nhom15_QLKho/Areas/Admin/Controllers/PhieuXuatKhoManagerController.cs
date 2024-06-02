using Microsoft.AspNetCore.Mvc;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhieuXuatKhoManagerController : Controller
    {
        private readonly IPhieuXuatKho _phieuXuatKhoRepository;

        public PhieuXuatKhoManagerController(IPhieuXuatKho phieuXuatKhoRepository)
        {
            _phieuXuatKhoRepository = phieuXuatKhoRepository;
        }

        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var phieuXuatKhos = await _phieuXuatKhoRepository.GetAllAsync();
            return View(phieuXuatKhos);
        }
        // Hiển thị form thêm sản phẩm mới

        public async Task<IActionResult> Add()
        {
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(PhieuXuatKho loaiHangHoa)
        {
            if (loaiHangHoa == null)
            {
                return BadRequest("LoaiHangHoa object is null");
            }

            if (ModelState.IsValid)
            {
                await _phieuXuatKhoRepository.AddAsync(loaiHangHoa);
                return RedirectToAction(nameof(Index));
            }
            return View(loaiHangHoa);
        }


        // Hiển thị form cập nhật sản phẩm

        public async Task<IActionResult> Update(int id)
        {
            var loaiHangHoa = await _phieuXuatKhoRepository.GetByIdAsync(id);
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
                var existingLoaiHangHoa = await _phieuXuatKhoRepository.GetByIdAsync(id);
                if (existingLoaiHangHoa == null)
                {
                    return NotFound();
                }

                // Cập nhật các thông tin khác của danh mục


                await _phieuXuatKhoRepository.UpdateAsync(existingLoaiHangHoa);

                return RedirectToAction(nameof(Index));
            }
            return View(loaiHangHoa);
        }


        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var phieuXuatKho = await _phieuXuatKhoRepository.GetByIdAsync(id);
            if (phieuXuatKho == null)
            {
                return NotFound();
            }
            return View(phieuXuatKho);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _phieuXuatKhoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int id)
        {
            var phieuXuatKho = await _phieuXuatKhoRepository.GetByIdAsync(id);
            if (phieuXuatKho == null)
            {
                return NotFound();
            }
            return View(phieuXuatKho);
        }

        public IActionResult Details(int id)
        {
            var CT_phieuXuatKhos = _phieuXuatKhoRepository.GetCT_PhieuXuatKhosByPhieuXuatKhoId(id);

            if (CT_phieuXuatKhos == null || !CT_phieuXuatKhos.Any())
            {
                return NotFound();
            }

            return View(CT_phieuXuatKhos);
        }
    }
}
