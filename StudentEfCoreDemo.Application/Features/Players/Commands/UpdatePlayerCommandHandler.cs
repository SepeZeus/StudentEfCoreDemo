using MediatR;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Players.Commands
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IPlayerRepository _playerRepository;

        public UpdatePlayerCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetPlayer(request.Id);
            if (player == null)
            {
                throw new KeyNotFoundException($"Player with Id {request.Id} not found.");
            }

            player.Age = request.Age;
            player.FirstName = request.FirstName;
            player.LastName = request.LastName;
            player.Position = request.Position;
            player.TeamId = request.TeamId;
            player.Goals = request.Goals;

            await _playerRepository.UpdatePlayer(player);
        }
    }
}
