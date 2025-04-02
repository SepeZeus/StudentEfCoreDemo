using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Players.Commands
{
    public record UpdatePlayerCommand : IRequest
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int TeamId { get; set; }
        public int Goals { get; set; }
    }
}
