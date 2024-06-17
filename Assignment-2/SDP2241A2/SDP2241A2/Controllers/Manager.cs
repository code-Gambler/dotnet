using AutoMapper;
using SDP2241A2.Data;
using SDP2241A2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-abc8902c-6f06-46e3-ad25-1823747dcd05
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace SDP2241A2.Controllers
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
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceWithDetailViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>();


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

        public IEnumerable<TrackWithDetailViewModel> TrackGetAll()
        {
            var allTrack = (from t in ds.Tracks
                        orderby t.Name
                        select t).Include(t => t.Album).Include(t => t.Genre);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(allTrack);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetBluesJazz()
        {
            var blueOrJazz = (from j in ds.Tracks
                       where j.GenreId == 2 || j.GenreId == 6
                       orderby j.Name
                       select j).Include(t => t.Album).Include(t => t.Genre);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(blueOrJazz);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetCantrellStaley()
        {
            var cantrellAndStaley = (from r in ds.Tracks
                              where r.Composer.Contains("Jerry Cantrell") && r.Composer.Contains("Layne Staley")
                              orderby r.Composer, r.Name
                              select r).Include(t => t.Album).Include(t => t.Genre);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(cantrellAndStaley);
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetTop50Longest()
        {
            var top50Long = (from l in ds.Tracks
                             orderby l.Milliseconds descending
                             select l).Include(t => t.Album).Include(t => t.Genre).Take(50);
            var sortedTop50Long = top50Long.OrderBy(l => l.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(sortedTop50Long);
        }
        public IEnumerable<TrackWithDetailViewModel> TrackGetTop50Smallest()
        {
            var top50Small = (from l in ds.Tracks
                             orderby l.Bytes ascending
                             select l).Include(t => t.Album).Include(t => t.Genre).Take(50);
            var sortedTop50Small = top50Small.OrderBy(l => l.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(sortedTop50Small);
        }

        public IEnumerable<InvoiceBaseViewModel> InvoiceGetAll()
        {
            var invoiceList = from i in ds.Invoices
                      orderby i.InvoiceDate descending
                      select i;

            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBaseViewModel>>(invoiceList);
        }
        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int? id)
        {
            var detailedInvoice = ds.Invoices.Include("Customer.Employee").Include("InvoiceLines.Track.Genre").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(i => i.InvoiceId == id);

            return mapper.Map<Invoice, InvoiceWithDetailViewModel>(detailedInvoice);
        }
    }
}