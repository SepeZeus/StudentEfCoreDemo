using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Teams.Commands
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamDto>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<CreateTeamDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team
            {
                Name = request.Name,
                SportType = request.SportType,
                FoundedDate = request.FoundedDate,
                HomeStadium = request.HomeStadium,
                MaxRosterSize = request.MaxRosterSize,
                Players = request.Players.Select(p => new Player
                {
                    Age = p.Age,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = p.Position,
                    TeamId = p.TeamId,
                    Goals = p.Goals
                }).ToList()
            };

            var createdTeam = await _teamRepository.AddTeam(team);
            return new CreateTeamDto
            {
                Id = createdTeam.Id,
                Name = createdTeam.Name,
                SportType = createdTeam.SportType,
                FoundedDate = createdTeam.FoundedDate,
                HomeStadium = createdTeam.HomeStadium,
                MaxRosterSize = createdTeam.MaxRosterSize,
                Players = createdTeam.Players.Select(p => new PlayerDto
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
        }
    }
}
