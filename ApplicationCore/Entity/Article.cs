using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ApplicationCore.Entity
{
    public class Article
    {
        public Article()
        {
            StockGroupsList = new List<StockGroup>();
        }

        public string Book { get; set; }
        /// <summary>
        /// returns the article code
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// returns the article description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// returns the article category
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// returns the item family
        /// </summary>
        public string Family { get; private set; }

        /// <summary>\
        /// returns the item Series
        /// </summary>
        public string Series { get; private set; }

        /// <summary>
        /// returns the Level1
        /// </summary>
        public string Level1 { get; private set; }

        /// <summary>
        /// returns the Level2
        /// </summary>
        public string Level2 { get; private set; }

        /// <summary>
        /// returns the style
        /// </summary>
        public string Style { get; private set; }

        /// <summary>
        /// Returns the alias name of the article 
        /// </summary>
        public string NameAlias { get; private set; }

        /// <summary>
        /// Returns the line of the article
        /// </summary>
        public string Line { get; private set; }

        /// <summary>
        /// Country of origin
        /// </summary>
        public string OriginCountryId { get; private set; }

        /// <summary>
        /// returns whether the article has photo or not
        /// </summary>
        public bool HasPhoto { get; private set; }

        /// <summary>
        /// returns whether the article has video or not
        /// </summary>
        public bool HasVideo { get { return !string.IsNullOrEmpty(Youtube); } }

        /// <summary>
        /// returns whether the article has chalco data
        /// </summary>
        public bool HasChalcoData { get; private set; }

        /// <summary>
        /// returns the bar code
        /// </summary>
        public string ItemBarCode { get; private set; }

        /// <summary>
        /// returns the article is in magento
        /// </summary>
        public bool IsMagentoEnabled { get; private set; }

        /// <summary>
        /// returns the stock quantity
        /// </summary>
        public int StockQuantity { get; private set; }

        /// <summary>
        /// returns the vendor stock quantity
        /// </summary>
        public int? VendorStockQuantity { get; private set; }


        /// <summary>
        /// returns the master quantity
        /// </summary>
        public int MasterQuantity { get; private set; }

        /// <summary>
        /// returns the vendor account
        /// </summary>
        public string VendorAccount { get; private set; }

        /// <summary>
        /// returns the primary vendor account
        /// </summary>
        public string PrimaryVendorAccount { get; private set; }

        /// <summary>
        /// returns the materials in csv
        /// </summary>
        public string Materials { get; private set; }

        /// <summary>
        /// returns the youtube video link 
        /// </summary>
        public string Youtube { get; private set; }

        /// <summary>
        /// returns the width
        /// </summary>
        public float Width { get; private set; }

        /// <summary>
        /// returns the height
        /// </summary>
        public float Height { get; private set; }

        /// <summary>
        /// returns the height
        /// </summary>
        public float Depth { get; private set; }

        /// <summary>
        /// returns the height
        /// </summary>
        public float Weight { get; private set; }

        /// <summary>
        /// returns true if the shop sign is Kasanova
        /// </summary>
        public bool IsKasanova { get; private set; }

        /// <summary>
        /// returns true if the shop sign is Kasanova+
        /// </summary>
        public bool IsKasanovaPiu { get; private set; }

        /// <summary>
        /// returns true if the shop sign is COIMPORT
        /// </summary>
        public bool IsCoimport { get; private set; }

        /// <summary>
        /// returns true if the shop sign is Italian Factory
        /// </summary>
        public bool IsItalianFactory { get; private set; }

        /// <summary>
        /// returns true if the shop sign is L'Outlet
        /// </summary>
        public bool IsOutlet { get; private set; }


        /// <summary>
        /// returns true if the shop sign is Casa Sull'Albero
        /// </summary>
        public bool IsCasaSullAlbero { get; private set; }

        /// <summary>
        /// returns true if the shop sign is Le Kikke
        /// </summary>
        public bool IsLeKikke { get; private set; }

        /// <summary>
        /// returns true if the shop sign is ECommerce
        /// </summary>
        public bool IsECommerce { get; private set; }


        private List<StockGroup> StockGroupsList { get; set; }
        /// <summary>
        /// returns the list of StockGroups with supply status
        /// </summary>
        public IReadOnlyCollection<StockGroup> StockGroups => new ReadOnlyCollection<StockGroup>(StockGroupsList.ToList());

        public void Add(StockGroup stockGroup)
        {
            StockGroupsList.Add(stockGroup);
        }


    }
}