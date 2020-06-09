using Chinook.Domain;
using Chinook.Domain.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;
using static Chinook.Common.StringHelper;

namespace Chinook.Schema.Types
{
    public class AlbumType : ObjectType<Album>
    {
        protected override void Configure(IObjectTypeDescriptor<Album> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field<AlbumType>(r => r.Artist(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(AlbumType.Artist)));

            descriptor.Field<AlbumType>(r => r.Tracks(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(AlbumType.Tracks)));
        }

        public Artist Artist([Service] ChinookContext context, [Parent] Album album)
        {
            var result = context.Artist.FirstOrDefault(e => e.ArtistId == album.ArtistId);

            return result;
        }

        public IQueryable<Track> Tracks([Service] ChinookContext context, [Parent] Album album)
        {
            var result = context.Track.Where(i => i.AlbumId == album.AlbumId);

            return result;
        }
    }
}
