
using System;

namespace WebApplication3.Models
{
    public class TaiKhoanDTO
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool TrangThai { get; set; }
        public DateOnly NgayThamGia { get; set; }
        public DateOnly? LanDangNhapCuoi { get; set; }

        // Nếu cần trả về các vai trò, bạn có thể thêm các thông tin liên quan đến vai trò.
        public IEnumerable<string> VaiTro { get; set; } = new List<string>();
    }
}
