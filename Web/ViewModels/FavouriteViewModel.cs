using System;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class FavouriteViewModel
    {
        public string Name { get; set; }

        public DateTime LastUpdate { get; set; }

        public List<ArticleLightViewModel> Articles { get; set; }

        public string User { get; set; }
    }
}