
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
	public class AppSettings
	{
		public AppSettings()
		{ }

            public string SpotifyLinks { get; set; }
			 public string YoutubeLinks { get; set; }
    }
}
