using System;

namespace Web.ViewModels
{
    public class ArticleLightViewModel
    {
        private const string PhotoBaseUrl = "~/api/images/{0}";

        public string Id { get; set; }

        public string Description { get; set; }

        public string NameAlias { get; set; }

        public string Materials { get; set; }

        public string Line { get; set; }

        public string PhotoUrl { get { return string.Format(PhotoBaseUrl, Id); } }

        public string LargePhotoUrl { get { return string.Concat(PhotoUrl, "/1"); } }

        public float Height { get; set; }

        public float Depth { get; set; }

        public float Width { get; set; }

        public float Weight { get; set; }

        public bool HasPhotoInChalco { get; set; }

        public decimal GrossSalesPrice { get; set; }

        public string GrossSalesPriceF { get { return string.Format("List. Acq. : € {0:N2}", GrossSalesPrice); } }

        public string StockArrivalDate { get; set; }

        public string StockArrivalQty { get; set; }

        public string Youtube { get; set; }

        public string Chalco
        {
            get
            {
                if (_chalco == null)
                {
                    _chalco = HasPhotoInChalco ? "~/Content/Images/camera.png" : "~/Content/Images/blank.gif";
                }
                return _chalco;
            }
        }
        private string _chalco;

        private string _dimensions;
        public string Dimensions
        {
            get
            {
                if (_dimensions == null)
                {
                    if (this.Width > 0 || this.Depth > 0 || this.Height > 0)
                    {
                        _dimensions = String.Format("{0:N0} x {1:N0} x {2:N0} (Cm)", this.Width, this.Depth, this.Height);
                    }
                    else
                    {
                        _dimensions = "";
                    }
                }
                return _dimensions;
            }
        }

        private string _weightInString;
        public string WeightInString
        {
            get
            {
                if (_weightInString == null)
                {
                    _weightInString = (Weight > 0) ? string.Format("{0:######,###} g", Weight) : "";
                }
                return _weightInString;
            }
        }

        private bool? _hasVideo;
        public bool HasVideo
        {
            get
            {
                if (!_hasVideo.HasValue)
                {
                    _hasVideo = Youtube != null && Youtube.Length > 0;
                }
                return _hasVideo.Value;
            }
        }

        public bool IsDirectDelivery { get; set; }

        public bool IsPrivateLabel { get; set; }

        public bool IsMandatoryPickup { get; set; }

        public decimal GrossRetailPrice { get; set; }

        public string GrossRetailPriceF { get { return string.Format("€ {0:N2}", GrossRetailPrice); } }

        public float RetailDiscountPercentage { get; set; }

        public string RetailDiscountPercentageF
        {
            get { return RetailDiscountPercentage != 0 ? $"Sc: {RetailDiscountPercentage:N2} %" : string.Empty; }
        }

        public decimal NetRetailPrice { get; set; }

        public string NetRetailPriceF { get { return string.Format("€ {0:N2}", NetRetailPrice); } }

        public string Video { get { return HasVideo ? "~/Content/Images/video.png" : "~/Content/Images/blank.gif"; } }

        public string DirectDelivery { get { return IsDirectDelivery ? "~/Content/Images/delivery.png" : "~/Content/Images/blank.gif"; } }

        public string PrivateLabel { get { return IsPrivateLabel ? "~/Content/Images/privatelabel.png" : "~/Content/Images/blank.gif"; } }

    }
}