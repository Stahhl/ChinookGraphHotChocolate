using Chinook.Domain;
using Chinook.Domain.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;
using static Chinook.Common.StringHelper;

namespace Chinook.Schema.Types
{
    public class TrackType : ObjectType<Track>
    {
        protected override void Configure(IObjectTypeDescriptor<Track> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field<TrackType>(r => r.Album(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(TrackType.Album)));

            descriptor.Field<TrackType>(r => r.Genre(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(TrackType.Genre)));

            descriptor.Field<TrackType>(r => r.MediaType(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(TrackType.MediaType)));

            descriptor.Field<TrackType>(r => r.InvoiceLines(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(TrackType.InvoiceLines)));

            descriptor.Field<TrackType>(r => r.PlaylistTracks(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(TrackType.InvoiceLines)));
        }

        public Album Album ([Service] ChinookContext context, [Parent] Track track)
        {
            var result = context.Album.FirstOrDefault(e => e.AlbumId == track.AlbumId);

            return result;
        }
        public Genre Genre([Service] ChinookContext context, [Parent] Track track)
        {
            var result = context.Genre.FirstOrDefault(e => e.GenreId == track.GenreId);

            return result;
        }
        public MediaType MediaType([Service] ChinookContext context, [Parent] Track track)
        {
            var result = context.MediaType.FirstOrDefault(e => e.MediaTypeId == track.MediaTypeId);

            return result;
        }
        public IQueryable<InvoiceLine> InvoiceLines([Service] ChinookContext context, [Parent] Track track)
        {
            var result = context.InvoiceLine.Where(i => i.TrackId == track.TrackId);

            return result;
        }
        public IQueryable<PlaylistTrack> PlaylistTracks([Service] ChinookContext context, [Parent] Track track)
        {
            var result = context.PlaylistTrack.Where(i => i.TrackId == track.TrackId);

            return result;
        }

    }
}
