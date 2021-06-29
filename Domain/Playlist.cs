using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Playlist
    {
        #region Properties
        [Key]
        public int Key { get; set; }
        public bool IsPublic { get; set; }
        public List<Media> MediaSites { get; set; }
        public AppUser User { get; set; }
        #endregion
    }
}
