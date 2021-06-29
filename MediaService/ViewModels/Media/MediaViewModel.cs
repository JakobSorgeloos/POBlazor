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
        public string  EmbeddedUrl { get; set; }
        public string  Title { get; set; }
        public string  NameUser { get; set; }
    }
}
