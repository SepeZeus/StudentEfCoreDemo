using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Players.Queries
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto?>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayerDto?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetPlayer(request.Id);
            if (player == null)
            {
                return null;
            }
            return new PlayerDto
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                Age = player.Age,
                Position = player.Position,
                TeamId = player.TeamId,
                Goals = player.Goals
            };
        }
    }
}
