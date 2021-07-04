using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository;
using Domain;
using Services.Interfaces;
using Services.ViewModels;
using Services.ViewModels.Media;
using Microsoft.Extensions.Options;

namespace Services.Service
{
    public class MediaService /*TODO : IMediaService*/
    {

        #region Property
        private readonly ApplicationDbContext _applicationDbContext;
        public  IOptions<AppSettings> _config { get; set; }

        #endregion

        #region Constructor
        public MediaService(ApplicationDbContext appDBContext, IOptions<AppSettings> config)
        {
            _applicationDbContext = appDBContext;
            _config = config;
        }
        #endregion

        #region Get List of Media
        public async Task<List<Media>> GetAllMedia()
        {
            return await _applicationDbContext.MediaSites.ToListAsync();
        }
        #endregion

        #region Insert Media
        public bool InsertMedia(AddMediaViewModel addMediaViewModel)

        {
            var media = ConvertAddVmToMedia(addMediaViewModel);
         
            _applicationDbContext.MediaSites.Add(media);
            _applicationDbContext.SaveChanges();
            return true;
        }
        #endregion

        #region Get Media by Id
        public async Task<Media> GetMedia(int Id)
        {
            var media = await _applicationDbContext.MediaSites.FirstOrDefaultAsync(c => c.Key.Equals(Id));
            return media;
        }
        #endregion

        #region Update Media


        public async Task<bool> UpdateMedia(int Id, EditMediaViewModel editMediaViewModel)
        {
            var media = await GetMedia(Id);
            media.IsPublic = editMediaViewModel.IsPublic;//Todo Add mapper
            media.Title = editMediaViewModel.Title;
            media.Url = editMediaViewModel.Url;
            media.EmbeddedUrl = EmbeddedUrlBuilder(editMediaViewModel.Url);

            _applicationDbContext.MediaSites.Update(media);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region DeleteMedia
        public async Task<bool> DeleteMediaAsync(int Id)

        {
            var media = await GetMedia(Id);
            _applicationDbContext.Remove(media);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Convert MediaList To ViewModelList
        public async Task<List<MediaViewModel>> GetAllMediaViewModels()
        {
            var medias = await GetAllMedia();
            var listvm = new List<MediaViewModel>();
            foreach (var media in medias)
            {
                listvm.Add(new MediaViewModel()
                {
                    Key = media.Key,
                    EmbeddedUrl = media.EmbeddedUrl,
                    AppUser = media.User,
                    Title = media.Title,
                    IsPublic = media.IsPublic,
                    Url = media.Url,
                    

                });
            }
            return listvm;
        }
        #endregion

        #region Convert AddMediaVM to Media
        public Media ConvertAddVmToMedia(AddMediaViewModel addMediaViewModel)
        {
            //create correct media Object, see Select MediaType based of input
            var media = new Music();

            //create correct embbed url spotify and youtube only at the moment
            media.EmbeddedUrl = EmbeddedUrlBuilder(addMediaViewModel.Url);

            media.Title = addMediaViewModel.Title;
            media.Url = addMediaViewModel.Url;
            media.IsPublic = addMediaViewModel.IsPublic;
            media.User = _applicationDbContext.Users.FirstOrDefault(_ => _.UserName == addMediaViewModel.Gebruiker);

            return media;
        }

        

        #endregion

        #region Convert EditMediaVM to Media & Media to EditMediaVM

        public async Task<Media> ConvertEditVmToMedia(EditMediaViewModel editMediaViewModel)
        {
            //create correct media Object, see Select MediaType based of input //ToDo: Overbodig, pas aan in logica
            var media = await GetMedia(editMediaViewModel.Key);

            //create correct embbed url spotify and youtube only at the moment
            media.EmbeddedUrl = EmbeddedUrlBuilder(editMediaViewModel.Url);

            media.Title = editMediaViewModel.Title;
            media.Url = editMediaViewModel.Url;
            media.IsPublic = editMediaViewModel.IsPublic;
           
            //TODO add user with login credentials.

            return media;

        }
       

        public async Task<EditMediaViewModel> ConvertMediaToEditVm(int Id)
        {
            var media = await GetMedia(Id);
            var editVm = new EditMediaViewModel()
            {
                Url = media.Url,
                IsPublic = media.IsPublic,
                Key = media.Key,
                Title = media.Title
            };

            
            return editVm;

        }

        

        #endregion

        #region Build Embedded Url
        public string EmbeddedUrlBuilder(string url)
        {

            
            string id = "";
            var z = "";
            if (url.Contains("you"))
            {
                var y = url.Split('?');
                var x = y[1].Split('&');
                id = x[0].Substring(2);
                z = _config.Value.YoutubeLinks + id;
                return z;
            }
            else
            {
                var y = url.Split(new string[] { ".com/" }, StringSplitOptions.None);
                var x = y[1].Split('?');
                id = x[0];
                z = _config.Value.SpotifyLinks + id;
                return (z);
            }
        }
        #endregion
    }
}
