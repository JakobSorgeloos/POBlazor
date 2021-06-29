using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        #region Properties
        //TODO add roles+ implement role usage
        public string Name { get; set; }
        public List<Media> Uploads { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Playlist> Playlists { get; set; }
        #endregion

        #region Constructor

        public AppUser()
        {

        }
        public AppUser(string userName):base(userName)
        {

        }
        
        #endregion
    }
}
