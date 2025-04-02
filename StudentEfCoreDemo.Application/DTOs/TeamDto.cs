﻿using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SportType { get; set; } = string.Empty;
        public DateTime FoundedDate { get; set; }
        public string HomeStadium { get; set; } = string.Empty;
        public int MaxRosterSize { get; set; }
        public ICollection<Player> Players { get; set; } = null!;
    }
}
