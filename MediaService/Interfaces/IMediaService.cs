using Domain;
using Services.ViewModels;
using Services.ViewModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMediaService
    {
        public Task<List<Media>> GetAllMedia();
        public bool InsertMedia(AddMediaViewModel addMediaViewModel);
        public Task<Media> GetMedia(int Id);
        public Task<bool> UpdateMedia(EditMediaViewModel editMediaViewModel);
        public Task<bool> DeleteMediaAsync(int Id);
        public Task<List<MediaViewModel>> GetAllMediaViewModels();
        public Task<Media> ConvertEditVmToMedia(EditMediaViewModel editMediaViewModel);
        public Task<EditMediaViewModel> ConvertMediaToEditVm(int Id);
        public string EmbeddedUrlBuilder(string url);
        public Task<bool> LikeSong(MediaViewModel media, string user);
        public Task<bool> CheckIfUserLikedMediaObject(MediaViewModel media, string user);

    }
}
