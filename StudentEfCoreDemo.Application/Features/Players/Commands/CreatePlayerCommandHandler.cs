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

        public CreatePlayerCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public async Task<PlayerDto> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Position = request.Position,
                TeamId = request.TeamId,
                Goals = request.Goals
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
