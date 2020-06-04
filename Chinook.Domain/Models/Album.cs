﻿using HotChocolate;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class Album
    {
        public Album()
        {
            Track = new HashSet<Track>();
        }

        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }

        [GraphQLIgnore]
        public virtual Artist Artist { get; set; }
        [GraphQLIgnore]
        public virtual ICollection<Track> Track { get; set; }
    }
}
