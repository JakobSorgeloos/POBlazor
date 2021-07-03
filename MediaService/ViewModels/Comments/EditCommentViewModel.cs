using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels.Comments
{
    public class EditCommentViewModel
    {
        public int  Key { get; set; }
        public string Title { get; set; }
        public string CommentText { get; set; }
    }
}
