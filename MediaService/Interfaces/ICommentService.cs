using Domain;
using Services.ViewModels;
using Services.ViewModels.Comments;
using Services.ViewModels.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommentService
    {
        public Task<List<Comment>> GetAllComments();
        public bool InsertComment(AddCommentViewModel addCommentViewModel);
        public Task<Comment> GetComment(int Id);
        public Task<bool> UpdateComment(int Id, EditCommentViewModel editCommentViewModel);
        public Task<bool> DeleteCommentAsync(int Id);
        public Task<List<AddCommentViewModel>> GetAllCommentViewModels();
        public Comment ConvertAddVmToComment(AddCommentViewModel addCommentViewModel);
        public Comment ConvertEditVmToMedia(EditCommentViewModel editCommentViewModel);
        public Task<EditCommentViewModel> ConvertCommentToEditVm(int Id);


    }
}
