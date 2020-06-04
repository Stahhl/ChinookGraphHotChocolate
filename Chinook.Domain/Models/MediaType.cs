using HotChocolate;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Track = new HashSet<Track>();
        }

        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        [GraphQLIgnore]
        public virtual ICollection<Track> Track { get; set; }
    }
}
