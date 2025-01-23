using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using AutoMapper;
using X.PagedList;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using PagedList.Core;

namespace WebApplication3.Controllers
{
    [Route("api/tai-khoan")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaiKhoanController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context; // Khởi tạo context từ DI
            _mapper = mapper;
        }

        private string GenerateJwtToken(TaiKhoan user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[32]; // 32 bytes = 256 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key); // Tạo khóa ngẫu nhiên
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.TenDangNhap),
                    new Claim("UserID", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = "localhost",
                Audience = "localhost",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            // Tìm tài khoản dựa trên tên đăng nhập
            var user = await _context.TaiKhoan
                .FirstOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.MatKhau, user.MatKhau))
            {
                return Unauthorized("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Lấy danh sách vai trò từ bảng trung gian TaiKhoan_VaiTro
            var roles = await _context.TaiKhoanVaiTroUsers
                .Where(tv => tv.TaiKhoanId == user.Id)
                .ToListAsync();

            // Tạo JWT token
            var token = GenerateJwtToken(user);

            // Trả về token và thông tin tài khoản
            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.TenDangNhap,
                    user.Email,
                    user.TrangThai,
                    VaiTro = roles.Select(role => role.VaiTro.TenVaiTro) // Trả về danh sách tên vai trò
                }
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchTaiKhoan(
            string? tenDangNhap = null,
            int? id = null,
            int page = 1,
            int pageSize = 10)
        {
            var query = _context.TaiKhoan.AsQueryable();

            query = query.Where(tk =>
                (string.IsNullOrEmpty(tenDangNhap) || tk.TenDangNhap.Contains(tenDangNhap)) &&
                (!id.HasValue || tk.Id == id.Value)
            );

            int totalData = await query.CountAsync();

            var pagedList = await Task.Run(() => query.OrderBy(tk => tk.Id).ToPagedList(page, pageSize));

            var dtoList = _mapper.Map<IEnumerable<TaiKhoanDTO>>(pagedList);

            return Ok(new
            {
                totalData,
                pageSize,
                pageIndex = page,
                data = dtoList
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostTaiKhoan([FromBody] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoan.MatKhau);
                _context.TaiKhoan.AddAsync(taiKhoan);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(SearchTaiKhoan), new { id = taiKhoan.Id }, taiKhoan);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiKhoan(int id, [FromBody] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.Id)
            {
                return BadRequest("ID không hợp lệ.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingTaiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (existingTaiKhoan == null)
            {
                return NotFound("Tài khoản không tồn tại.");
            }
            if (!string.IsNullOrEmpty(taiKhoan.MatKhau) &&
                taiKhoan.MatKhau != existingTaiKhoan.MatKhau)
            {
                existingTaiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoan.MatKhau);
            }
            _mapper.Map(taiKhoan, existingTaiKhoan);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound("Tài khoản không tồn tại.");
            }
            _context.TaiKhoan.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
