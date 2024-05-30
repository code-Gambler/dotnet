using AutoMapper;
using SDP224A1.Data;
using SDP224A1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-69f71889-d654-46a2-b5d3-eef334c8cde2
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace SDP224A1.Controllers
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
                cfg.CreateMap<Concert, ConcertBaseViewModel>();
                cfg.CreateMap<ConcertAddViewModel, Concert>();
                cfg.CreateMap<ConcertEditViewModel, Concert>()
                   .ForMember(dest => dest.Name, opt => opt.Ignore()); // Ignore Name because it should not be edited
                cfg.CreateMap<Concert, ConcertEditFormViewModel>();
                cfg.CreateMap<ConcertBaseViewModel, ConcertEditFormViewModel>();

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

        // Method to get all concerts
        public IEnumerable<ConcertBaseViewModel> ConcertGetAll()
        {
            var concerts = ds.Concerts.OrderBy(c => c.Name);
            return mapper.Map<IEnumerable<Concert>, IEnumerable<ConcertBaseViewModel>>(concerts);
        }

        // Method to get a concert by id
        public ConcertBaseViewModel ConcertGetById(int id)
        {
            var concert = ds.Concerts.Find(id);
            return (concert == null) ? null : mapper.Map<Concert, ConcertBaseViewModel>(concert);
        }

        // Method to add a new concert
        public ConcertBaseViewModel ConcertAdd(ConcertAddViewModel newConcert)
        {
            try
            {
                var addedConcert = ds.Concerts.Add(mapper.Map<ConcertAddViewModel, Concert>(newConcert));
                ds.SaveChanges();
                return mapper.Map<Concert, ConcertBaseViewModel>(addedConcert);
            }
            catch
            {
                return null;
            }
        }

        // Method to edit an existing concert
        public ConcertBaseViewModel ConcertEdit(ConcertEditViewModel editedConcert)
        {
            var o = ds.Concerts.Find(editedConcert.ConcertId); // Assumes ConcertEditViewModel has a ConcertId property

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(editedConcert);
                ds.SaveChanges();
                return mapper.Map<Concert, ConcertBaseViewModel>(o);
            }
        }

        // Method to delete an existing concert
        public bool ConcertDelete(int id)
        {
            var concertToDelete = ds.Concerts.Find(id);
            if (concertToDelete == null)
            {
                return false;
            }
            try
            {
                // Remove the object
                ds.Concerts.Remove(concertToDelete);
                ds.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}