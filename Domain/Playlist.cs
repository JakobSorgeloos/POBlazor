using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Playlist
    {
        [Key]
        public int Key { get; set; }
        public bool IsPublic { get; set; }
        public List<Media> MediaSites { get; set; }
        public AppUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

    }
}
