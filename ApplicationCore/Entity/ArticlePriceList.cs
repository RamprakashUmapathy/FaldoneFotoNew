using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entity
{
    public class ArticlePrice
    {
        public string ArticleId { get; private set; }

        public string ShopSignId { get; private set; }

        public bool IsPrivateLabel { get; private set; }

        public bool IsDirectDelivery { get; private set; }

        public bool IsMandatoryPickup { get; private set; }

        public string TagName { get; private set; }

        /// <summary>
        /// returns the gross retail price 
        /// </summary>
        public decimal GrossRetailPrice { get; private set; }

        /// <summary>
        /// returns the gross sales price (L1)
        /// </summary>
        public decimal GrossSalesPrice { get; private set; }

        public decimal NetRetailPrice { get; private set; }

        public float RetailDiscountPercentage { get; private set; }
    }
}
