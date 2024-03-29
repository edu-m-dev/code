﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace wwi.bl.EF
{
    public partial class SpecialDeal
    {
        public int SpecialDealID { get; set; }
        public int? StockItemID { get; set; }
        public int? CustomerID { get; set; }
        public int? BuyingGroupID { get; set; }
        public int? CustomerCategoryID { get; set; }
        public int? StockGroupID { get; set; }
        public string DealDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? UnitPrice { get; set; }
        public int LastEditedBy { get; set; }
        public DateTime LastEditedWhen { get; set; }

        public virtual BuyingGroup BuyingGroup { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerCategory CustomerCategory { get; set; }
        public virtual Person LastEditedByNavigation { get; set; }
        public virtual StockGroup StockGroup { get; set; }
        public virtual StockItem StockItem { get; set; }
    }
}