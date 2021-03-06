﻿using HotChocolate;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Track = new HashSet<Track>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }

        [GraphQLIgnore]
        public virtual ICollection<Track> Track { get; set; }
    }
}
