using System;

namespace ApplicationCore.Entity
{
    public class StockGroup : IEquatable<StockGroup>
    {
        /// <summary>
        /// article id
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// stock group id
        /// </summary>
        public string StockGroupId { get; set; }

        /// <summary>
        /// Last supplying status Id
        /// </summary>
        public string LastSupplyStatusId { get; set; }

        public bool Equals(StockGroup other)
        {
            if (other == null) return false;
            return this.StockGroupId == other.StockGroupId;
        }
    }
}
