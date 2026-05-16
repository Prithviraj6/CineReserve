using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Domain.Entities;
using CineReserve.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CineReserve.Infrastruture.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
