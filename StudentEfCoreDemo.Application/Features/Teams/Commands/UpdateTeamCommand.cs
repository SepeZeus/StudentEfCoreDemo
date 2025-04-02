﻿using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Teams.Commands
{
    public record UpdateTeamCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SportType { get; set; }
        public DateTime FoundedDate { get; set; }
        public string HomeStadium { get; set; }
        public int MaxRosterSize { get; set; }
        public ICollection<PlayerDto> Players { get; set; }
    }
}
