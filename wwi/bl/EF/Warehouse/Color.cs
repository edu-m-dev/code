﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace wwi.bl.EF
{
    public partial class Color
    {
        public Color()
        {
            StockItems = new HashSet<StockItem>();
        }

        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public int LastEditedBy { get; set; }

        public virtual Person LastEditedByNavigation { get; set; }
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}