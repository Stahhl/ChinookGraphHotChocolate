using Chinook.Domain;
using Chinook.Domain.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Chinook.Common.StringHelper;

namespace Chinook.Schema.Types
{
    public class PlaylistType : ObjectType<Playlist>
    {
        protected override void Configure(IObjectTypeDescriptor<Playlist> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field<PlaylistType>(r => r.Tracks(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(PlaylistType.Tracks)));
        }

        public IQueryable<Track> Tracks([Service] ChinookContext context, [Parent] Playlist playlist)
        {
            var result = context.PlaylistTrack.Where(i => i.PlaylistId == playlist.PlaylistId).Select(x => x.Track);

            return result;
        }
    }
}
