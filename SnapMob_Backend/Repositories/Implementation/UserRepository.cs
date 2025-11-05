using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Data;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace SnapMob_Backend.Repositories.implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task BlockUnblockUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) return;

            user.IsBlocked = !user.IsBlocked;
            user.ModifiedOn = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) return;

            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
