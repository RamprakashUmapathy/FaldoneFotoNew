using ApplicationCore.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Articles = new List<ArticleLightViewModel>();
        }

        public string SearchText { get; set; }

        public string SearchField { get; set; }

        public string SearchOption { get; set; }

        private string _shopSignId;
        public string ShopSignId
        {
            get { return _shopSignId ?? "K"; } //Default KASANOVA
            set { _shopSignId = value; }
        }

        public IEnumerable<SelectListItem> ShopSigns { get; set; }

        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public string FamilyId { get; set; }

        public IEnumerable<SelectListItem> Families { get; set; }

        public string SeriesId { get; set; }

        public IEnumerable<SelectListItem> Series { get; set; }

        public string Level1Id { get; set; }

        public IEnumerable<SelectListItem> Level1s { get; set; }

        public string Level2Id { get; set; }

        public IEnumerable<SelectListItem> Level2s { get; set; }

        public string WebEnabledId { get; set; }

        public IEnumerable<SelectListItem> WebEnabledItems { get; set; }

        public string VideoAvailableId { get; set; }

        public IEnumerable<SelectListItem> VideoAvailableItems { get; set; }

        public string WareHouseNameId { get; set; }

        public IEnumerable<SelectListItem> WareHouseItems { get; set; }

        public string ChalcoId { get; set; }

        public IEnumerable<SelectListItem> ChalcoItems { get; set; }

        public string PhotoId { get; set; }

        public IEnumerable<SelectListItem> PhotoItems { get; set; }

        public string ColorId { get; set; }

        public IEnumerable<SelectListItem> ColorItems { get; set; }

        public string TagId { get; set; }

        public IEnumerable<SelectListItem> TagItems { get; set; }

        public string StyleId { get; set; }

        public IEnumerable<SelectListItem> StyleItems { get; set; }

        public double PriceRangeFrom { get; set; }

        public double PriceRangeTo { get; set; }

        public double DiscountFrom { get; set; }

        public double DiscountTo { get; set; }

        public double StockQty { get; set; }

        public bool IsCardView { get; set; }

        public User User { get; set; }

        public IEnumerable<ArticleLightViewModel> Articles { get; set; }


    }
}