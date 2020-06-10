using Chinook.Domain.Models;
using Chinook.Schema.Filters;
using Chinook.Schema.Types;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Schema.Querys
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            #region Customer
            descriptor.Field(t => t.Customer(default, default))
                .Type<NonNullType<CustomerType>>();

            descriptor.Field(t => t.Customers(default))
                .Type<ListType<NonNullType<CustomerType>>>()
                .UseFiltering();
            #endregion

            #region Artist
            descriptor.Field(t => t.Artist(default, default))
                .Type<NonNullType<ArtistType>>();

            descriptor.Field(t => t.Artists(default))
                .Type<ListType<NonNullType<ArtistType>>>()
                .UseFiltering();
            #endregion

            #region Invoice
            descriptor.Field(t => t.Invoice(default, default))
                .Type<NonNullType<InvoiceType>>();

            descriptor.Field(t => t.Invoices(default))
                .Type<ListType<NonNullType<InvoiceType>>>()
                .UseFiltering();
            #endregion

            #region InvoiceLine
            descriptor.Field(t => t.InvoiceLine(default, default))
                .Type<NonNullType<InvoiceLineType>>();

            descriptor.Field(t => t.InvoiceLines(default))
                .Type<ListType<NonNullType<InvoiceLineType>>>()
                .UseFiltering();
            #endregion

            #region Employee
            descriptor.Field(t => t.Employee(default, default))
                .Type<NonNullType<EmployeeType>>();

            descriptor.Field(t => t.Employees(default))
                .Type<ListType<NonNullType<EmployeeType>>>()
                .UseFiltering();
            #endregion

            #region Track
            descriptor.Field(t => t.Track(default, default))
                .Type<NonNullType<TrackType>>();

            descriptor.Field(t => t.Tracks(default))
                .Type<ListType<NonNullType<TrackType>>>()
                .UseFiltering();
            #endregion

            #region Album
            descriptor.Field(t => t.Album(default, default))
                .Type<NonNullType<AlbumType>>();

            descriptor.Field(t => t.Albums(default))
                .Type<ListType<NonNullType<AlbumType>>>()
                .UseFiltering();
            #endregion

            #region Playlist
            descriptor.Field(t => t.Playlist(default, default))
                .Type<NonNullType<PlaylistType>>();

            descriptor.Field(t => t.Playlists(default))
                .Type<ListType<NonNullType<PlaylistType>>>()
                .UseFiltering();
            #endregion

        }
    }
}
