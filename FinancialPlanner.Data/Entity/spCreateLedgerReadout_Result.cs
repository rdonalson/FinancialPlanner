//------------------------------------------------------------------------------
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
    
    public partial class spCreateLedgerReadout_Result
    {
        public int PkLMain { get; set; }
        public Nullable<System.DateTime> WDate { get; set; }
        public Nullable<double> CreditSummary { get; set; }
        public Nullable<double> DebitSummary { get; set; }
        public Nullable<double> NetDaily { get; set; }
        public Nullable<double> RunningTotal { get; set; }
        public Nullable<int> ItemType { get; set; }
        public Nullable<int> FkItemDetail { get; set; }
        public string PeriodName { get; set; }
        public string Name { get; set; }
        public Nullable<double> Amount { get; set; }
    }
}
