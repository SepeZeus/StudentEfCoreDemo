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
    public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, List<PlayerDto?>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayersQueryHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }   
        public async Task<List<PlayerDto>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetPlayers();
            return players.Select(player => new PlayerDto
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                Age = player.Age,
                Position = player.Position,
                TeamId = player.TeamId,
                Goals = player.Goals
            }).ToList();
        }
    }
}
