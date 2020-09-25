using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Models;

namespace WEB_HOCTIENGANH.Data
{
    public class AuthRepository: IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            // Chú ý rằng user có bốn thuộc tính: Id, UserName, PasswordHash, PasswordSalt
            byte[] passwordHash, passwordSalt; // Một mảng byte
            // Truyền hai biến trên vào hàm CreatePasswordHash. Từ khóa out sẽ nhận giá trị vào sau đó reset giá trị và lưu giữ giá trị khi ra khỏi hàm.
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Khởi tạo hmac là một thể hiện mới của lớp HMACSHA512 với một key được tạo random
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // Một key random dùng để giải mã passwordHash về lại thành password.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // passwordHash là giá trị sau khi được mã hóa từ password
            }
        }

        // UserExists có một tham số username thực hiện việc kiểm tra xem có bất cứ row nào trong entity Users hay không.
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
            {
                return true;
            }

            return false;
        }
    }
}
