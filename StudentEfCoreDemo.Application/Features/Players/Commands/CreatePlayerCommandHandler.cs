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

namespace StudentEfCoreDemo.Application.Features.Players.Commands
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, PlayerDto>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public CreatePlayerCommandHandler(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }
        public async Task<PlayerDto> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetTeam(request.TeamId);
            if (team == null)
            {
                throw new KeyNotFoundException($"Player can't join Team with Id {request.TeamId} not found.");
            }

            if (team.Players.Count >= team.MaxRosterSize)
            {
                throw new InvalidOperationException($"Team with Id {request.TeamId} has reached its maximum roster size.");
            }

            var player = new Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Position = request.Position,
                TeamId = request.TeamId,
                Goals = request.Goals,
            };

            var createdPlayer = await _playerRepository.AddPlayer(player);
            return new PlayerDto
            {
                Id = createdPlayer.Id,
                FirstName = createdPlayer.FirstName,
                LastName = createdPlayer.LastName,
                Age = createdPlayer.Age,
                Position = createdPlayer.Position,
                TeamId = createdPlayer.TeamId,
                Goals = createdPlayer.Goals
            };
        }
    }
}
