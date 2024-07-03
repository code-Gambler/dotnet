using AutoMapper;
using SDP2241A3.Data;
using SDP2241A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-7a0707a6-84a4-445b-9ca1-3048195c3196
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace SDP2241A3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackWithDetailViewModel, Track>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<TrackAddFormViewModel, TrackAddViewModel>();
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();
                cfg.CreateMap<Playlist, PlaylistWithTracksViewModel>();
                cfg.CreateMap<Playlist, PlaylistEditTracksViewModel>();
                cfg.CreateMap<PlaylistWithTracksViewModel, PlaylistEditTracksFormViewModel>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albumList = ds.Albums.OrderBy(a => a.Title);
            return albumList == null ? null : mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albumList);
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artistList = ds.Artists.OrderBy(a => a.Name);
            return artistList == null ? null : mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artistList);
        }

        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            var mediaTypeList = ds.MediaTypes.OrderBy(m => m.Name);
            return mediaTypeList == null ? null : mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(mediaTypeList);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetail()
        {
            var trackListWithDetail = ds.Tracks.Include("Album.Artist").Include("Album").Include("MediaType").OrderBy(t => t.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(trackListWithDetail);
        }
        public TrackWithDetailViewModel TrackGetById(int id)
        {
            var selectedTrack = ds.Tracks.Include("Album.Artist").Include("Album").Include("MediaType").FirstOrDefault(t => t.TrackId == id);
            return selectedTrack == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(selectedTrack);
        }

        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newItem)
        {
            var album = ds.Albums.Find(newItem.AlbumId);
            var mediaType = ds.MediaTypes.Find(newItem.MediaTypeId);
            if (album == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));
                addedItem.Album = album;
                addedItem.MediaType = mediaType;
                ds.SaveChanges();
                return (addedItem == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedItem);
            }
        }

        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            var listOfPlayList = ds.Playlists.Include("Tracks").OrderBy(p => p.Name);
            return listOfPlayList == null ? null : mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(listOfPlayList);
        }
        public PlaylistWithTracksViewModel PlaylistGetById(int id)
        {
            var currentPlaylist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == id);
            return currentPlaylist == null ? null : mapper.Map<Playlist, PlaylistWithTracksViewModel>(currentPlaylist);
        }
        public PlaylistEditTracksViewModel PlaylistEditTracks(PlaylistEditTracksViewModel newItem)
        {
            var editedItem = ds.Playlists.Include("Tracks").SingleOrDefault(t => t.PlaylistId == newItem.PlaylistId);

            if (editedItem == null)
            {
                return null;
            }
            else
            {
                editedItem.Tracks.Clear();
                foreach (var item in newItem.TrackIds)
                {
                    var a = ds.Tracks.Find(item);
                    editedItem.Tracks.Add(a);
                }
                ds.SaveChanges();
                return mapper.Map<Playlist, PlaylistEditTracksViewModel>(editedItem);
            }
        }
    }
}