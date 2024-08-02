using AutoMapper;
using SDP2241A5.Data;
using SDP2241A5.Models;
using SDP2241A5.Views.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V2 == 2241-6b47e737-c2fc-40d4-ada1-203b56c69a75
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace SDP2241A5.Controllers
{
    public class Manager
    {

        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Genre, GenresBaseViewModel>();

                cfg.CreateMap<Actor, ActorBaseViewModel>();
                cfg.CreateMap<Actor, ActorWithShowInfoViewModel>();
                cfg.CreateMap<ActorAddViewModel, Actor>();

                cfg.CreateMap<Show, ShowBaseViewModel>();
                cfg.CreateMap<Show, ShowWithInfoViewModel>();
                cfg.CreateMap<ShowAddViewModel, Show>();

                cfg.CreateMap<Episode, EpisodeBaseViewModel>();
                cfg.CreateMap<Episode, EpisodeWithShowNameViewModel>();
                cfg.CreateMap<EpisodeAddViewModel, Episode>();
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

        //Genre
        public IEnumerable<GenresBaseViewModel> GenreGetAll()
        {
            var genres = ds.Genres.OrderBy(g => g.Name);

            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenresBaseViewModel>>(genres);
        }

        //Actor
        public IEnumerable<ActorBaseViewModel> ActorGetAll()
        {
            var actors = ds.Actors.OrderBy(a => a.Name);

            return mapper.Map<IEnumerable<Actor>, IEnumerable<ActorBaseViewModel>>(actors);
        }

        public ActorBaseViewModel ActorGetById(int id)
        {
            var actor = ds.Actors.Find(id);
            return (actor == null) ? null : mapper.Map<Actor, ActorBaseViewModel>(actor);
        }

        public ActorWithShowInfoViewModel ActorGetByIdWithDetail(int id)
        {
            var detailedActor = ds.Actors.Include("Shows")
                .SingleOrDefault(a => a.Id == id);

            return mapper.Map<Actor, ActorWithShowInfoViewModel>(detailedActor);
        }
        public ActorBaseViewModel ActorAdd(ActorAddViewModel newActor)
        {

            newActor.Executive = HttpContext.Current.User.Identity.Name;
            var addedActor = ds.Actors.Add(mapper.Map<ActorAddViewModel, Actor>(newActor));
            ds.SaveChanges();
            return (addedActor == null) ? null : mapper.Map<Actor, ActorBaseViewModel>(addedActor);
        }

        //Show
        public IEnumerable<ShowBaseViewModel> ShowGetAll()
        {
            var shows = ds.Shows.OrderBy(s => s.Name);

            return mapper.Map<IEnumerable<Show>, IEnumerable<ShowBaseViewModel>>(shows);
        }

        public ShowWithInfoViewModel ShowGetByIdWithDetail(int id)
        {
            var detailedShow = ds.Shows.Include("Episodes").Include("Actors")
                        .SingleOrDefault(s => s.Id == id);

            return mapper.Map<Show, ShowWithInfoViewModel>(detailedShow);
        }
        public ShowWithInfoViewModel ShowAdd(ShowAddViewModel newItem)
        {

            var actor = ds.Actors.Find(newItem.ActorId);

            if (actor == null)
            {
                return null;
            }
            else
            {
                var addedShow = ds.Shows.Add(mapper.Map<ShowAddViewModel, Show>(newItem));
                addedShow.Coordinator = HttpContext.Current.User.Identity.Name;
                addedShow.Actors = new List<Actor> { actor };

                actor.Shows.Add(addedShow);
                ds.Shows.Add(addedShow);
                ds.SaveChanges();

                return (addedShow == null) ? null : mapper.Map<Show, ShowWithInfoViewModel>(addedShow);
            }
        }

        //Episode
        public IEnumerable<EpisodeWithShowNameViewModel> GetAllEpisodesWithShowNames()
        {
            var episodes = ds.Episodes
                .Include("Show")
                .OrderBy(e => e.Show.Name)
                .ThenBy(e => e.SeasonNumber)
                .ThenBy(e => e.EpisodeNumber)
                .ToList();

            return mapper.Map<IEnumerable<EpisodeWithShowNameViewModel>>(episodes);
        }

        public EpisodeWithShowNameViewModel EpisodeGetByIdWithDetail(int id)
        {
            var detailedEpisode = ds.Episodes.Include("Show")
                .SingleOrDefault(e => e.Id == id);

            return mapper.Map<Episode, EpisodeWithShowNameViewModel>(detailedEpisode);
        }

        public EpisodeWithShowNameViewModel EpisodeAdd(EpisodeAddViewModel newEpisode)
        {
            var show = ds.Shows.Find(newEpisode.ShowId);
            var addedEpisode = ds.Episodes.Add(mapper.Map<EpisodeAddViewModel, Episode>(newEpisode));
            addedEpisode.Clerk = HttpContext.Current.User.Identity.Name;
            addedEpisode.Show = show;
            ds.SaveChanges();

            return (addedEpisode == null) ? null : mapper.Map<Episode, EpisodeWithShowNameViewModel>(addedEpisode);

        }

        // *** Add your methods ABOVE this line **

        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // Write a method for each entity and remember to check for existing
        // data first.  You will call this/these method(s) from a controller action.
        public bool LoadRoles()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim() { Name = "Admin" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Clerk" });

                //check back

                // Add additional role claims here

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool LoadGenres()
        {
            bool done = false;
            if (ds.Genres.Count() == 0)
            {
                ds.Genres.Add(new Genre { Name = "Action" });
                ds.Genres.Add(new Genre { Name = "Adventure" });
                ds.Genres.Add(new Genre { Name = "Comedy" });
                ds.Genres.Add(new Genre { Name = "Drama" });
                ds.Genres.Add(new Genre { Name = "Horror" });
                ds.Genres.Add(new Genre { Name = "Musical" });
                ds.Genres.Add(new Genre { Name = "Romance" });
                ds.Genres.Add(new Genre { Name = "Sci-Fi" });
                ds.Genres.Add(new Genre { Name = "Documentary" });
                ds.Genres.Add(new Genre { Name = "Thriller" });

                ds.SaveChanges();
                done = true;
            }
            return done;
        }

        public bool LoadActors()
        {
            bool done = false;
            if (ds.Actors.Count() == 0)
            {
                ds.Actors.Add(new Actor
                {
                    Name = "Robert Downey Junior",
                    AlternateName = "RDJ",
                    BirthDate = new DateTime(1965, 4, 4),
                    Height = 1.74m,
                    ImageUrl = "https://mim.p7s1.io/pis/ld/51cczChLCVyZ-c1vEwXZAanB-DJ78rnq2V2gRWTHzVV6NywNWMSJCEGmZ4YSENP-WRL0b9lo2z_IfR_W_NB3xh-Ior85TZvb7pimSFJsEaUCErKPqDrp2BOJSWQcImFCSxLyXk3ybRg/profile:original?w=400",
                    Executive = User.Name
                });

                ds.Actors.Add(new Actor
                {
                    Name = "Anne Hathaway",
                    AlternateName = "Annie",
                    BirthDate = new DateTime(1982, 11, 12),
                    Height = 1.73m,
                    ImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTTPqsTBEwd6QUluxycfWH-k7S7gPA1tRt4lisrlPb5tBCkJeru",
                    Executive = User.Name
                });

                ds.Actors.Add(new Actor
                {
                    Name = "James Hugh Calum Laurie CBE",
                    AlternateName = "Hugh Laurie",
                    BirthDate = new DateTime(1959, 6, 11),
                    Height = 1.88m,
                    ImageUrl = "https://celebrityinside.com/wp-content/uploads/2015/08/Hugh-Laurie.jpg",
                    Executive = User.Name
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool LoadShows()
        {
            bool done = false;
            var hughLaurie = ds.Actors.SingleOrDefault(a => a.Name == "James Hugh Calum Laurie CBE");
            var rdj = ds.Actors.SingleOrDefault(a => a.Name == "Robert Downey Junior");

            if (hughLaurie == null || rdj == null)
            {
                return false; 
            }

            if (ds.Shows.Count() == 0)
            {
                ds.Shows.Add(new Show
                {
                    Name = "House",
                    Genre = "Drama",
                    ReleaseDate = new DateTime(2004, 11, 16),
                    ImageUrl = "https://resizing.flixster.com/SwqtqXI5oqwqK-_dd1MOloWTfIc=/ems.cHJkLWVtcy1hc3NldHMvdHZzZWFzb24vUlRUVjU0Mzg1LndlYnA=",
                    Coordinator = User.Name,
                    Actors = new Actor[] { hughLaurie }

                });

                ds.Shows.Add(new Show
                {
                    Name = "The Age of A.I.",
                    Genre = "Documentary",
                    ReleaseDate = new DateTime(2019, 12, 19),
                    ImageUrl = "https://i.ytimg.com/vi/UwsrzCVZAb8/maxresdefault.jpg",
                    Coordinator = User.Name,
                    Actors = new Actor[] { rdj }
                });
                ds.SaveChanges();
                done = true;
            }

            return done;
        }
        public bool LoadEpisodes()
        {

            bool done = false;
            var housemd = ds.Shows.SingleOrDefault(s => s.Name == "House");
            if (housemd == null) { return false; }

            var ageIfAI = ds.Shows.SingleOrDefault(s => s.Name == "The Age of A.I.");
            if (ageIfAI == null) { return false; }

            if (ds.Episodes.Count() == 0) 
            {
                ds.Episodes.Add(new Episode
                {
                    Name = "Three Stories",
                    SeasonNumber = 1,
                    EpisodeNumber = 21,
                    Genre = "Drama",
                    AirDate = new DateTime(2005, 5, 17),
                    ImageUrl = "https://i.ytimg.com/vi/pjX_WhUsdJo/maxresdefault.jpg",
                    Clerk = User.Name,
                    Show = housemd
                });
                ds.Episodes.Add(new Episode
                {
                    Name = "Autopsy",
                    SeasonNumber = 2,
                    EpisodeNumber = 2,
                    Genre = "Drama",
                    AirDate = new DateTime(2005, 9, 20),
                    ImageUrl = "https://i.ytimg.com/vi/zstffidwHbw/maxresdefault.jpg",
                    Clerk = User.Name,
                    Show = housemd
                });

                ds.Episodes.Add(new Episode
                {
                    Name = "Broken",
                    SeasonNumber = 6,
                    EpisodeNumber = 1,
                    Genre = "Drama",
                    AirDate = new DateTime(2009, 9, 20),
                    ImageUrl = "https://fandomania.com/wp-content/uploads/2009/09/house601-03.jpg",
                    Clerk = User.Name,
                    Show = housemd
                });


                ds.Episodes.Add(new Episode
                {
                    Name = "How Far is Too Far?",
                    SeasonNumber = 1,
                    EpisodeNumber = 1,
                    Genre = "Documentary",
                    AirDate = new DateTime(2019, 12, 18),
                    ImageUrl = "https://img.youtube.com/vi/UwsrzCVZAb8/maxresdefault.jpg",
                    Clerk = User.Name,
                    Show = ageIfAI
                });
                ds.Episodes.Add(new Episode
                {
                    Name = "Healed through A.I.",
                    SeasonNumber = 1,
                    EpisodeNumber = 2,
                    Genre = "Documentary",
                    AirDate = new DateTime(2019, 12, 18),
                    ImageUrl = "https://img.youtube.com/vi/V5aZjsWM2wo/maxresdefault.jpg",
                    Clerk = User.Name,
                    Show = ageIfAI
                });
                ds.Episodes.Add(new Episode
                {
                    Name = "Using A.I. to build a better human",
                    SeasonNumber = 1,
                    EpisodeNumber = 3,
                    Genre = "Documentary",
                    AirDate = new DateTime(2019, 12, 18),
                    ImageUrl = "https://img.youtube.com/vi/lrv8ga02VNg/maxresdefault.jpg",
                    Clerk = User.Name,
                    Show = ageIfAI
                });
                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        //TODO: change back

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }

                // Remove additional entities as needed.

                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    #endregion

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }

        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }

        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }

        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion

}