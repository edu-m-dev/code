﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace wwi.bl.EF
{
    public partial class GetStockHoldingUpdatesResult
    {
        [Column("Quantity On Hand")]
        public int QuantityOnHand { get; set; }
        [Column("Bin Location")]
        public string BinLocation { get; set; }
        [Column("Last Stocktake Quantity")]
        public int LastStocktakeQuantity { get; set; }
        [Column("Last Cost Price")]
        public decimal LastCostPrice { get; set; }
        [Column("Reorder Level")]
        public int ReorderLevel { get; set; }
        [Column("Target Stock Level")]
        public int TargetStockLevel { get; set; }
        [Column("WWI Stock Item ID")]
        public int WWIStockItemID { get; set; }
    }
}
