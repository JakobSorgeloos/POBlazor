﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository;
using Domain;
using Services.Interfaces;
using Services.ViewModels;
using Services.ViewModels.Media;

namespace Services.Service
{
    public class PlaylistService
    {


        #region Property
        private readonly ApplicationDbContext _applicationDbContext;
        #endregion

        #region Constructor
        public PlaylistService(ApplicationDbContext appDBContext)
        {
            _applicationDbContext = appDBContext;
        }
        #endregion

        #region Get List of PlayList
        public async Task<List<Playlist>> GetAllPlaylists()
        {
            return await _applicationDbContext.Playlists.ToListAsync();
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
            media.IsPublic = editMediaViewModel.IsPublic;
            media.Title = editMediaViewModel.Title;
            media.Url = editMediaViewModel.Url;
            media.EmbeddedUrl = EmbeddedUrlBuilder(editMediaViewModel.Url);

            _applicationDbContext.MediaSites.Update(media);
            _applicationDbContext.SaveChanges();
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
            var medias = await GetAllPlaylists();
            var listvm = new List<MediaViewModel>();
           
            return listvm;
        }
        #endregion

        #region Convert AddMediaVM to Media
        public Media ConvertAddVmToMedia(AddMediaViewModel addMediaViewModel)
        {
            //create correct media Object, see Select MediaType based of input
            var media = MediaSwitcheroo(addMediaViewModel.MediaType);

            //create correct embbed url spotify and youtube only at the moment
            media.EmbeddedUrl = EmbeddedUrlBuilder(addMediaViewModel.Url);

            media.Title = addMediaViewModel.Title;
            media.Url = addMediaViewModel.Url;
            media.IsPublic = addMediaViewModel.IsPublic;
            //TODO add user with login credentials.

            return media;
        }

        #region Select MediaType based of input
        private Media MediaSwitcheroo(string type)
        {
            Media media;
            switch (type)
            {
                case "Podcast":
                    media = new Podcast();
                    return media;
                case "Music":
                    media = new Music();
                    return media;
                case "Movie":
                    media = new Film();
                    return media;
                case "Serie":
                default:
                    media = new Serie();
                    return media;
            }
        }
        #endregion

        #endregion

        #region Convert EditMediaVM to Media & Media to EditMediaVM

        public Media ConvertEditVmToMedia(EditMediaViewModel editMediaViewModel)
        {
            //create correct media Object, see Select MediaType based of input
            var media = MediaSwitcheroo(editMediaViewModel.MediaType);

            //create correct embbed url spotify and youtube only at the moment
            media.EmbeddedUrl = EmbeddedUrlBuilder(editMediaViewModel.Url);

            media.Title = editMediaViewModel.Title;
            media.Url = editMediaViewModel.Url;
            media.IsPublic = editMediaViewModel.IsPublic;
            media.Key = editMediaViewModel.Key;
            //TODO add user with login credentials.

            return media;

        }
        //public async Task<Media> ConvertEditVmToMedia(int Id, EditMediaViewModel editMediaViewModel)
        //{
        //    var media = await GetMedia(Id);


        //    //create correct embbed url spotify and youtube only at the moment
        //    media.EmbeddedUrl = EmbeddedUrlBuilder(editMediaViewModel.Url);

        //    media.Title = editMediaViewModel.Title;
        //    media.Url = editMediaViewModel.Url;
        //    media.IsPublic = editMediaViewModel.IsPublic;
        //   //TODO add user with login credentials.

        //    return media;

        //}

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

            editVm.MediaType = SelectMediaString(media);
            return editVm;

        }

        #region Select Mediatype String
        private string SelectMediaString(Media media)
        {
            switch (media)
            {
                case Podcast:
                    return "Podcast";
                case Music:
                    return "Music";
                case Film:
                    return "Movie";
                case Serie:
                default:
                    return "Serie";
            }
        }

        #endregion

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
                z = "https://www.youtube.com/embed/" + id;
                return z;

            }
            else
            {
                var y = url.Split(new string[] { ".com/" }, StringSplitOptions.None);
                var x = y[1].Split('?');
                id = x[0];
                z = "https://open.spotify.com/embed/" + id;
                return (z);
            }

        }
        #endregion
    }

}
    
