using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Players.Queries
{
    public record GetPlayersQuery : IRequest<List<PlayerDto?>>;
}
