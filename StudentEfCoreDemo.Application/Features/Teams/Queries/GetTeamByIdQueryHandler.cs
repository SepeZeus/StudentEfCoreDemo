using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Teams.Queries
{
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamDto?>
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamByIdQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<TeamDto?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetTeam(request.Id);
            if (team == null)
            {
                return null;
            }
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                SportType = team.SportType,
                FoundedDate = team.FoundedDate,
                HomeStadium = team.HomeStadium,
                MaxRosterSize = team.MaxRosterSize,
                Players = team.Players
            };
        }
    }
}
