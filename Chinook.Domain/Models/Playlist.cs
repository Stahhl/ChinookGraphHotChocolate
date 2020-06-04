using HotChocolate;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class Playlist
    {
        public Playlist()
        {
            PlaylistTrack = new HashSet<PlaylistTrack>();
        }

        public int PlaylistId { get; set; }
        public string Name { get; set; }

        [GraphQLIgnore]
        public virtual ICollection<PlaylistTrack> PlaylistTrack { get; set; }
    }
}
