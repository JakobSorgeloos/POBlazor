using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Review
    {
        #region Properties
        [Key]
        public int Key { get; set; }
        public string Tekst { get; set; }
        public AppUser Gebruiker { get; set; }
        public float Rating { get; set; }
        public Media Media { get; set; }
        #endregion
    }
}