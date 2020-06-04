using HotChocolate;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class PlaylistTrack
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

        [GraphQLIgnore]
        public virtual Playlist Playlist { get; set; }
        [GraphQLIgnore]
        public virtual Track Track { get; set; }
    }
}
