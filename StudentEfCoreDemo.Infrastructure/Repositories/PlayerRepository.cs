using Microsoft.EntityFrameworkCore;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly StudentContext _context;

        public PlayerRepository(StudentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task<Player> AddPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task UpdatePlayer(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }
    }
}
