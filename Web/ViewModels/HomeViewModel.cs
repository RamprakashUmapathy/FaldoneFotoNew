using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [Display(Description = "Insegna")]
        public string ShopSignId
        {
            get { return _shopSignId ?? "K"; } //Default KASANOVA
            set { _shopSignId = value; }
        }
        private string _shopSignId;

        public IEnumerable<SelectListItem> ShopSigns { get; set; }

        [Display(Name ="Categoria")]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Famiglia")]
        public string FamilyId { get; set; }

        public IEnumerable<SelectListItem> Families { get; set; }

        [Display(Name = "Serie")]
        public string SeriesId { get; set; }

        public IEnumerable<SelectListItem> Series { get; set; }

        [Display(Name = "Livello1")]
        public string Level1Id { get; set; }

        public IEnumerable<SelectListItem> Level1s { get; set; }

        [Display(Name = "Livello2")]
        public string Level2Id { get; set; }

        public IEnumerable<SelectListItem> Level2s { get; set; }

        [Display(Name = "Disponibile sul web")]
        public string WebEnabledId { get; set; }

        public IEnumerable<SelectListItem> WebEnabledItems { get; set; }

        [Display(Name = "Video disponibile")]
        public string VideoAvailableId { get; set; }

        public IEnumerable<SelectListItem> VideoAvailableItems { get; set; }

        [Display(Name = "Magazzino")]
        public string WareHouseNameId { get; set; }

        public IEnumerable<SelectListItem> WareHouseItems { get; set; }

        [Display(Name = "Presenti in Chalco")]
        public string ChalcoId { get; set; }

        public IEnumerable<SelectListItem> ChalcoItems { get; set; }

        [Display(Name = "Articoli con foto")]
        public string PhotoId { get; set; }

        public IEnumerable<SelectListItem> PhotoItems { get; set; }

        [Display(Name = "Tipo Colore")]
        public string ColorId { get; set; }

        public IEnumerable<SelectListItem> ColorItems { get; set; }

        [Display(Name = "Tag Commerciale")]
        public string TagId { get; set; }

        public List<string> TagItems { get; set; }

        [Display(Name = "Stile")]
        public string StyleId { get; set; }

        public List<string> StyleItems { get; set; }

        [Display(Name = "Prezzo da:")]
        public double PriceRangeFrom { get; set; }

        [Display(Name = "Prezzo a:")]
        public double PriceRangeTo { get; set; }

        [Display(Name = "Sconto minimo(%) da:")]
        public double DiscountFrom { get; set; }

        [Display(Name = "Sconto minimo(%) a:")]
        public double DiscountTo { get; set; }

        [Display(Name = "Disponibiltà da:")]
        public double StockQty { get; set; }

        public bool IsCardView { get; set; }

        public User User { get; set; }

        public List<ArticleLightViewModel> Articles { get; set; }

        [Display(Name = "Assortimento")]
        public string StockGroupId { get; set; }

        public List<string> StockGroupItems { get; set; }
        //public IEnumerable<string> StockGroupItems { get; set; }

        [Display(Name = "Approvvigionamento")]
        public string SupplyStatusId { get; set; }

        public List<string> SupplyStatusItems { get; set; }

        [Display(Name = "Consegna diretta")]
        public string DirectDeliveryId { get; set; }

        public IEnumerable<SelectListItem> DirectDeliveryItems { get; set; }

        [Display(Name = "Ritiro Obbligatorio")]
        public string MandatoryDeliveryId { get; set; }

        public IEnumerable<SelectListItem> MandatoryDeliveryItems { get; set; }

        public bool? RoundPanelCollapseState { get; set; }

    }
}