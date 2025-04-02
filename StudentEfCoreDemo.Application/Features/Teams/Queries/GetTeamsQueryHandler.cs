﻿using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Teams.Queries
{
    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, List<TeamDto?>>
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamsQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.GetTeams();
            return teams.Select(team => new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                SportType = team.SportType,
                FoundedDate = team.FoundedDate,
                HomeStadium = team.HomeStadium,
                MaxRosterSize = team.MaxRosterSize,
                Players = team.Players
            }).ToList();
        }
    }
}
