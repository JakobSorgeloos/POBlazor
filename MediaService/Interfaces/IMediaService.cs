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
        public List<Media> GetAllMedia();
        public bool InsertMedia(AddMediaViewModel addMediaViewModel);
        public Media GetMedia(int Id);
        public bool UpdateMedia(int Id, EditMediaViewModel editMediaViewModel);
        public Task<bool> DeleteMediaAsync(Media media);
        public List<MediaViewModel> GetMediaViewModels(List<Media> medias);
        public Media ConvertAddVmToMedia(AddMediaViewModel addMediaViewModel);
        public string EmbeddedUrlBuilder(string url);
        public Media ConvertEditVmToMedia(EditMediaViewModel editMediaViewModel);
        public EditMediaViewModel ConvertMediaToEditVm(Media media);


    }
}
