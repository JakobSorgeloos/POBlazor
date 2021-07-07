using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels.Media
{
    public class MediaViewModel
    {
        public int Key { get; set; }
        public string EmbeddedUrl { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public string Url { get; set; }
        public bool IsPublic { get; set; }

        public AppUser AppUser { get; set; }

        public float GemRating { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Playlist> Playlists { get; set; }

        public bool Liked { get; set; } = false;

    }
}
