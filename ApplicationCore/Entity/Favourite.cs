using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entity
{
    public class Favourite
    {

        public Favourite()
        {
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public DateTime LastUpdate { get; set; }

        private string Articles { get; set; }

        public IEnumerable<string> SelectedArticles { get { return Articles.Split(',').ToList(); } }

    }
}
