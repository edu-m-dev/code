﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace wwi.bl.EF
{
    public partial class SupplierTransaction
    {
        public int SupplierTransactionId { get; set; }
        public int SupplierId { get; set; }
        public int TransactionTypeId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal AmountExcludingTax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal OutstandingBalance { get; set; }
        public DateTime? FinalizationDate { get; set; }
        public bool? IsFinalized { get; set; }
        public int LastEditedBy { get; set; }
        public DateTime LastEditedWhen { get; set; }

        public virtual Person LastEditedByNavigation { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}