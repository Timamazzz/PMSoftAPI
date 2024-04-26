using DataAccess.Models;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository(ApplicationContext context) : ICrudRepository<User>
{
    public async Task<User?> CreateAsync(User user)
    {
        await context.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetByIdAsync(int? id)
    {
        var user = await context.Users.FindAsync(id);
        return user;
    }
    
    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> UpdateAsync(User user)
    {
        context.Update(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        User? user = await GetByIdAsync(id);

        if (user != null)
        {
            context.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}