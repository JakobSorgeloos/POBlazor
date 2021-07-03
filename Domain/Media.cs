using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Media
    {
        #region Properties

        [Key]
        public int Key { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        public string EmbeddedUrl { get; set; }

        [Required]
        [DataType(DataType.Url, ErrorMessage = "Gelieve een geldige Url in te geven")]
        public string Url { get; set; }

        
        [Display(Name = "Publiek")]

        public bool IsPublic { get; set; } = true;


        public List<Review> Review { get; set; }
        public AppUser User { get; set; }

        [Range(0, 5)]
        public float GemRating { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Playlist> Playlists { get; set; }
        #endregion

        #region Comments
        //Abstracte klasse die gebruikt wordt om de mediaobjecten in 1 lijst te kunnen steken (overzichtspagina)
        //Staan veel algemene properties in
        
       
       

        //TODO: Specifieke properties per mediatype aanmaken bvb: Serie heeft episodes en seizoenen
        #endregion
    }
}
