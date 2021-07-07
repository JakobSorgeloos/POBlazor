using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository;
using Domain;
using Services.Interfaces;
using Services.ViewModels;
using Services.ViewModels.Comments;

namespace Services.Service
{
    public class CommentService : ICommentService
    {
        #region Property
        private readonly ApplicationDbContext _applicationDbContext;
        #endregion

        #region Constructor
        public CommentService(ApplicationDbContext appDBContext)
        {
            _applicationDbContext = appDBContext;
        }
        #endregion

        #region Get List of Comments
        public async Task<List<Comment>> GetAllComments()
        {
            return await _applicationDbContext.Comments.Include(_ => _.Gebruiker).ToListAsync();
        }
        #endregion

        #region Insert Comment
        public bool InsertComment(AddCommentViewModel addCommentViewModel)

        {
            var comment = ConvertAddVmToComment(addCommentViewModel);
            _applicationDbContext.Comments.Add(comment);
            _applicationDbContext.SaveChanges();
            return true;
        }
        #endregion

        #region Get Comment by Id
        public async Task<Comment> GetComment(int Id)
        {
            var comment = await _applicationDbContext.Comments.FirstOrDefaultAsync(c => c.Key.Equals(Id));
            return comment;
        }
        #endregion

        #region Update Comment


        public async Task<bool> UpdateComment(int Id, EditCommentViewModel editCommentViewModel)
        {
            var comment = await GetComment(Id);
            comment.CommentText = editCommentViewModel.CommentText;
            comment.Title = editCommentViewModel.Title;


            _applicationDbContext.Comments.Update(comment);
            _applicationDbContext.SaveChanges();
            return true;
        }
        #endregion

        #region Delete Comment
        public async Task<bool> DeleteCommentAsync(int Id)

        {
            var comment = await GetComment(Id);
            _applicationDbContext.Remove(comment);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Convert CommentList To ViewModelList
        public async Task<List<AddCommentViewModel>> GetAllCommentViewModels()
        {
            var comments = await GetAllComments();
            var listvm = new List<AddCommentViewModel>();
            foreach (var comment in comments)
            {
                listvm.Add(new AddCommentViewModel()
                {
                    Gebruiker = comment.Gebruiker.Initials,
                    CommentText = comment.CommentText,
                    Title = comment.Title
                }
                );
            }
            return listvm;
        }
        #endregion

        #region Convert AddCommentVM to Comment
        public Comment ConvertAddVmToComment(AddCommentViewModel addCommentViewModel)
        {
            var media = _applicationDbContext.MediaSites.FirstOrDefault(_ => _.Key == addCommentViewModel.MediaKey);
            var comment = new Comment()
            {
                Title = addCommentViewModel.Title,
                CommentText = addCommentViewModel.CommentText,
                Gebruiker = _applicationDbContext.Users.FirstOrDefault(_ => _.UserName == addCommentViewModel.Gebruiker),
                Media = media
            };
            return comment;
        }
        #endregion

        #region Convert EditCommentVM to Comment & vice versa

        public Comment ConvertEditVmToMedia(EditCommentViewModel editCommentViewModel)
        {
            var comment = new Comment()
            {
                Title = editCommentViewModel.Title,
                CommentText = editCommentViewModel.CommentText,

            };

            return comment;
        }

        public async Task<EditCommentViewModel> ConvertCommentToEditVm(int Id)
        {
            var comment = await GetComment(Id);
            var editVm = new EditCommentViewModel()
            {
                CommentText = comment.CommentText,
                Title = comment.Title,
                Key = comment.Key
            };


            return editVm;

        }


        #endregion

    }
}

