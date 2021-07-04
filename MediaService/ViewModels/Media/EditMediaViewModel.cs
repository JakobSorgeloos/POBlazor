using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels.Media
{
    public class EditMediaViewModel
    {
        #region properties
        public int Key { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsPublic { get; set; }
        public string Gebruiker { get; set; }
       
        #endregion
    }
}

