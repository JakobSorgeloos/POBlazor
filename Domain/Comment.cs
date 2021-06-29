using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment
    {
        #region Properties
        [Key]
        public int Key { get; set; }
        public string CommentText { get; set; }
        public string Title { get; set; }
        public Media Media { get; set; }
        public AppUser Gebruiker { get; set; }
        #endregion

    }

}
