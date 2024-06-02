using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom15_QLKho.Models;
using Nhom15_QLKho.Repositories;

namespace Nhom15_QLKho.Controllers
{
	public class HangHoaController : Controller
	{
		private readonly IHangHoa _hangHoaRepository;
		private readonly ILoaiHangHoa _loaiHangHoaRepository;
		private readonly INhaCungCap _nhaCungCapRepository;
        private readonly IKho _khoRepository;
		public HangHoaController(IHangHoa hanghoaRepository,
		ILoaiHangHoa loaiHangHoaRepository, INhaCungCap nhaCungCapRepository, IKho khoRepository)
		{
			_hangHoaRepository = hanghoaRepository;
			_loaiHangHoaRepository = loaiHangHoaRepository;
			_nhaCungCapRepository = nhaCungCapRepository;
            _khoRepository = khoRepository;
			
		}
		// Hiển thị danh sách sản phẩm
		public async Task<IActionResult> Display(int id)
		{
			var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
			if (hangHoa == null)
			{
				return NotFound();
			}
			return View(hangHoa);
		}

		public async Task<IActionResult> Index(string searchString)
		{
			var hangHoas = _hangHoaRepository.GetAll();

			if (!String.IsNullOrEmpty(searchString))
			{
				hangHoas = hangHoas.Where(s => s.TenHH.Contains(searchString));
			}

			return View(await hangHoas.ToListAsync());
		}

        public async Task<IActionResult> Add()
        {
            var loaiHangHoas = await _loaiHangHoaRepository.GetAllAsync();
            var nhaCungCaps = await _nhaCungCapRepository.GetAllAsync();
            var Khos = await _khoRepository.GetAllAsync();

            ViewBag.loaiHangHoas = new SelectList(loaiHangHoas, "Id", "Name");
            ViewBag.nhaCungCaps = new SelectList(nhaCungCaps, "Id", "Name");
            ViewBag.Khos = new SelectList(Khos, "Id", "Name");

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

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); //

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }
    }
}
