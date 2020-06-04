using HotChocolate;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class Track
    {
        public Track()
        {
            InvoiceLine = new HashSet<InvoiceLine>();
            PlaylistTrack = new HashSet<PlaylistTrack>();
        }

        public int TrackId { get; set; }
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        [GraphQLIgnore]
        public virtual Album Album { get; set; }
        [GraphQLIgnore]
        public virtual Genre Genre { get; set; }
        [GraphQLIgnore]
        public virtual MediaType MediaType { get; set; }
        [GraphQLIgnore]
        public virtual ICollection<InvoiceLine> InvoiceLine { get; set; }
        [GraphQLIgnore]
        public virtual ICollection<PlaylistTrack> PlaylistTrack { get; set; }
    }
}
