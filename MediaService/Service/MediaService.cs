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
    public class MediaService : IMediaService
    {

        #region Property
        private readonly ApplicationDbContext _applicationDbContext;
        public IOptions<AppSettings> _config { get; set; }

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
            return await _applicationDbContext.MediaSites.Include(media => media.User.LikedSongs.MediaSites).Include(media => media.Comments).ToListAsync();
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
            //include nodig voor object appuser
            var media = await _applicationDbContext.MediaSites.Include("User").FirstOrDefaultAsync(c => c.Key.Equals(Id));
            return media;
        }
        #endregion

        #region Update Media

        public async Task<bool> UpdateMedia(EditMediaViewModel editMediaViewModel)
        {
            var media = await GetMedia(editMediaViewModel.Key);
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

        #region LikeSongPart
        public async Task<bool> LikeSong(MediaViewModel media, string user)
        {
            var mediaDbItem = await GetMedia(media.Key);
            var AppUser = _applicationDbContext.Users.Include(appUser => appUser.LikedSongs).FirstOrDefault(_ => _.UserName == user);
            var playlist = AppUser.LikedSongs;

            if (playlist.MediaSites == null)
            {
                playlist = new Playlist()
                {
                    MediaSites = new List<Media>(),
                    User = AppUser
                };
                mediaDbItem.LikesCount++;

                playlist.MediaSites.Add(mediaDbItem);
                _applicationDbContext.Playlists.Add(playlist);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            if (playlist.MediaSites.Contains(mediaDbItem))
            {
                mediaDbItem.LikesCount--;
                playlist.MediaSites.Remove(mediaDbItem);
                _applicationDbContext.Playlists.Update(playlist);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                mediaDbItem.LikesCount++;
                playlist.MediaSites.Add(mediaDbItem);
                _applicationDbContext.Playlists.Update(playlist);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> CheckIfUserLikedMediaObject(MediaViewModel media, string user)
        {
            var mediaDbItem = await GetMedia(media.Key);
            var appUser = _applicationDbContext.Users.Include(appUser => appUser.LikedSongs.MediaSites)
                .FirstOrDefault(_ => _.UserName == user);
            if (appUser != null && appUser.LikedSongs.MediaSites.Contains(mediaDbItem))
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Converters
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
                    Likes = media.LikesCount,
                    Comments = media.Comments
                });
            }
            return listvm;
        }

        public Media ConvertAddVmToMedia(AddMediaViewModel addMediaViewModel)
        {

            var media = new Media();

            //create correct embbed url spotify and youtube only at the moment
            media.EmbeddedUrl = EmbeddedUrlBuilder(addMediaViewModel.Url);

            media.Title = addMediaViewModel.Title;
            media.Url = addMediaViewModel.Url;
            media.IsPublic = addMediaViewModel.IsPublic;
            media.User = _applicationDbContext.Users.FirstOrDefault(_ => _.UserName == addMediaViewModel.Gebruiker);

            return media;
        }

        public async Task<Media> ConvertEditVmToMedia(EditMediaViewModel editMediaViewModel)
        {

            var media = await GetMedia(editMediaViewModel.Key);

            //create correct embbed url spotify and youtube only at the moment
            media.EmbeddedUrl = EmbeddedUrlBuilder(editMediaViewModel.Url);

            media.Title = editMediaViewModel.Title;
            media.Url = editMediaViewModel.Url;
            media.IsPublic = editMediaViewModel.IsPublic;

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

            if (string.IsNullOrEmpty(url)) return "";
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
            else if (url.Contains("spotify"))
            {
                var y = url.Split(new string[] { ".com/" }, StringSplitOptions.None);
                var x = y[1].Split('?');
                id = x[0];
                z = _config.Value.SpotifyLinks + id;
                return (z);
            }
            else return "";
        }
        #endregion
    }
}
