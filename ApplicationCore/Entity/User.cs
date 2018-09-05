using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entity
{
    public class User
    {
        public IPrincipal Principal { get; private set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public List<string> AssortmentsList { get { return Assortments.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries).ToList(); } }
        private string Assortments { get; set; }
        public bool CanViewShopSigns { get; set; }
        public bool CanViewMagento { get; set; }
        public bool CanViewChalco { get; set; }
        public bool CanViewOther { get { return CanViewAssortment || CanViewSupplyStatus || CanViewTagCommericial || CanViewStyle; } }
        public bool CanViewTagCommericial { get; set; }
        public bool CanViewStyle { get; set; }
        public bool CanViewAssortment { get; set; }
        public bool CanViewSupplyStatus { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsKasanovaUser { get; set; }
        public bool IsExternalUser { get; set; }

    }
}
