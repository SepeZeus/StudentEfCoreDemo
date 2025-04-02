using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(int id);
        Task<Team> AddTeam(Team team);
        Task UpdateTeam(Team team);
        Task DeleteTeam(int id);
    }
}
