﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using wwi.bl.EF;

namespace wwi.bl.EF
{
    public partial class WwiDbContext
    {

        [DbFunction("CalculateCustomerPrice", "Website")]
        public static decimal? CalculateCustomerPrice(int? CustomerID, int? StockItemID, DateTime? PricingDate)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        [DbFunction("DetermineCustomerAccess", "Application")]
        public IQueryable<DetermineCustomerAccessResult> DetermineCustomerAccess(int? CityID)
        {
            return FromExpression(() => DetermineCustomerAccess(CityID));
        }

        protected void OnModelCreatingGeneratedFunctions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetermineCustomerAccessResult>().HasNoKey();
        }
    }
}
