﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace wwi.hr.EF
{
    public partial class StockItemStockGroup
    {
        public int StockItemStockGroupId { get; set; }
        public int StockItemId { get; set; }
        public int StockGroupId { get; set; }
        public int LastEditedBy { get; set; }
        public DateTime LastEditedWhen { get; set; }

        public virtual People LastEditedByNavigation { get; set; }
        public virtual StockGroup StockGroup { get; set; }
        public virtual StockItem StockItem { get; set; }
    }
}