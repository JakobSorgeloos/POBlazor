using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels.Comments
{
    public class AddCommentViewModel
    {
        public string CommentText { get; set; }
        public string Gebruiker { get; set; }
        public string Title { get; set; }
        public int MediaKey { get; set; }

    }
}
