using MediatR;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Teams.Commands
{
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {

            var team = new Team
            {
                Id = request.Id,
                Name = request.Name,
                SportType = request.SportType,
                FoundedDate = request.FoundedDate,
                HomeStadium = request.HomeStadium,
                MaxRosterSize = request.MaxRosterSize,
                Players = request.Players.Select(p => new Player
                {
                    Id = p.Id,
                    Age = p.Age,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = p.Position,
                    TeamId = p.TeamId,
                    Goals = p.Goals
                }).ToList()
            };

            await _teamRepository.UpdateTeam(team);
        }
    }
}
