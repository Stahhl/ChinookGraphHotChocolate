using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Album = new HashSet<Album>();
        }

        public int ArtistId { get; set; }
        public string Name { get; set; }

        [GraphQLIgnore]
        public virtual ICollection<Album> Album { get; set; }
    }
}
