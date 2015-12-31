﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinancialPlanner.Data.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ItemDetailEntities : DbContext
    {
        public ItemDetailEntities()
            : base("name=ItemDetailEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Credit> Credits { get; set; }
        public virtual DbSet<Debit> Debits { get; set; }
        public virtual DbSet<InitialAmount> InitialAmounts { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
    
        public virtual ObjectResult<spCreateLedgerReadout_Result> spCreateLedgerReadout(Nullable<System.DateTime> timeFrameBegin, Nullable<System.DateTime> timeFrameEnd, string userName)
        {
            var timeFrameBeginParameter = timeFrameBegin.HasValue ?
                new ObjectParameter("TimeFrameBegin", timeFrameBegin) :
                new ObjectParameter("TimeFrameBegin", typeof(System.DateTime));
    
            var timeFrameEndParameter = timeFrameEnd.HasValue ?
                new ObjectParameter("TimeFrameEnd", timeFrameEnd) :
                new ObjectParameter("TimeFrameEnd", typeof(System.DateTime));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spCreateLedgerReadout_Result>("spCreateLedgerReadout", timeFrameBeginParameter, timeFrameEndParameter, userNameParameter);
        }
    }
}
