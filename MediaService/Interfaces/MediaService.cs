//using ProjectOefening.Domain;
//using ProjectOefening.Repository;
//using ProjectOefening.ServiceLayer.Interfaces;
//using ProjectOefening.ServiceLayer.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProjectOefening.ServiceLayer.Services
//{
//   public class MediaService : IMediaService
//    {
//        private readonly ApplicationDbContext _applicationDbContext;
//            public MediaService(ApplicationDbContext applicationDbContext)
//        {
//            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
//        }

//        public void AddMedia(AddMediaViewModel addMediaViewModel)
//        {
//            var temp = MediaSwitcheroo(addMediaViewModel.MediaType);
//            temp.Url = addMediaViewModel.Url;
//            temp.EmbeddedUrl = EmbeddedUrlBuilder(addMediaViewModel.Url);
//            temp.IsPublic = addMediaViewModel.IsPublic;
//            temp.Title = addMediaViewModel.Title;
//            _applicationDbContext.MediaSites.Add(temp);
//            _applicationDbContext.SaveChanges();
//        }

//        IEnumerable<IndexMediaViewModel> IMediaService.CreateIndexVm()
//        {
//            List<IndexMediaViewModel> vm = new List<IndexMediaViewModel>();

//            foreach (var item in _applicationDbContext.MediaSites)
//            {
//                vm.Add(new IndexMediaViewModel()
//                {
//                    EmbeddedUrl = item.EmbeddedUrl,
//                    Key = item.Key,
//                    NameUploader = "jakob",
//                    GemRating = 3f,
//                    CountComments = 17,
//                    CountReviews = 15
                    
//                });

//            }


//            return vm;
//        }
//       
//        public AddMediaViewModel EditViewModel(int? id)
//        {
//            var mediaSite = _applicationDbContext.MediaSites.FirstOrDefault(_ => _.Key == id);
            
//            var vm = new AddMediaViewModel()
//            {
//                Url = mediaSite.Url,
//                Gebruiker = mediaSite.Gebruiker.Name,
//                IsPublic = mediaSite.IsPublic,
//                MediaType = MediaTypeStringGen(mediaSite)
//            };

//            return vm;
//        }

//        public void EditMediaItem(int id, AddMediaViewModel addMediaViewModel)
//        {
//            var temp = MediaSwitcheroo(addMediaViewModel.MediaType);
//            addMediaViewModel.MediaType = 
//            temp.EmbeddedUrl = EmbeddedUrlBuilder(addMediaViewModel.Url);
//            temp.IsPublic = addMediaViewModel.IsPublic;
//            temp.Key = id;

//            _applicationDbContext.Update(temp);
//            _applicationDbContext.SaveChanges();

//        }




//        public DetailMediaViewModel DeleteDetails(int? id)
//        {
//            var mediaSite =  _applicationDbContext.MediaSites.FirstOrDefault(m => m.Key == id);

//            var vm = new DetailMediaViewModel()
//            {
//                Key = mediaSite.Key,
//                EmbeddedUrl = mediaSite.EmbeddedUrl,
//                Gebruiker = mediaSite.Gebruiker.Name,
//                Title = mediaSite.Title
//            };

//            return vm;
//        }

        
        
//        public void Delete(int id)
//        {
//            var mediaSite = _applicationDbContext.MediaSites.Find(id);
//            _applicationDbContext.MediaSites.Remove(mediaSite);
//            _applicationDbContext.SaveChangesAsync();
//        }

//        private bool MediaSiteExists(int id)
//        {
//            return _applicationDbContext.MediaSites.Any(e => e.Key == id);
//        }
    









//    private string EmbeddedUrlBuilder(string url)
//        {
//            string id = "";
//            var z = "";
//            if (url.Contains("you"))
//            {

//                var y = url.Split('?');

//                var x = y[1].Split('&');
//                id = x[0].Substring(2);
//                z = "https://www.youtube.com/embed/" + id;
//                return z;

//            }
//            else 
//            {
//                var y = url.Split(new string[] { ".com/" }, StringSplitOptions.None);
//                var x = y[1].Split('?');
//                id = x[0];
//                z = "https://open.spotify.com/embed/" + id;
//                return (z);
//            }
           
//        }

//        private MediaSite MediaSwitcheroo(string type)
//        {
//            MediaSite media;
//            switch (type)
//            {
//                case "Podcast":
//                    media = new Podcast();
//                    return media;
//                case "Muziek":
//                    media = new Muziek();
//                    return media;
//                case "Film":
//                    media = new Film();
//                    return media;
//                case "Serie":
//                default:
//                    media = new Serie();
//                    return media;
//            }

            
//        }

//        private string MediaTypeStringGen(MediaSite mediaSite)
//        {
//            switch(mediaSite)
//            {
//                case Podcast:
//                    return "Podcast";
//                case Muziek:
//                    return "Muziek";
//                case Film:
//                    return "Film";
//                case Serie:
//                default:
//                    return "Serie";

//            }
//        }
//   }
//}
