using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Interfaces
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayers();
        Task<Player> GetPlayer(int id);
        Task<Player> AddPlayer(Player player);
        Task UpdatePlayer(Player player);
        Task DeletePlayer(int id);
    }
}
