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
    using System.Collections.Generic;
    
    public partial class vwCredit
    {
        public int PkCredit { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> FkPeriod { get; set; }
        public string PeriodName { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> WeeklyDOW { get; set; }
        public Nullable<int> EverOtherWeekDOW { get; set; }
        public Nullable<int> BiMonthlyDay1 { get; set; }
        public Nullable<int> BiMonthlyDay2 { get; set; }
        public Nullable<int> MonthlyDOM { get; set; }
        public Nullable<int> Quarterly1Month { get; set; }
        public Nullable<int> Quarterly1Day { get; set; }
        public Nullable<int> Quarterly2Month { get; set; }
        public Nullable<int> Quarterly2Day { get; set; }
        public Nullable<int> Quarterly3Month { get; set; }
        public Nullable<int> Quarterly3Day { get; set; }
        public Nullable<int> Quarterly4Month { get; set; }
        public Nullable<int> Quarterly4Day { get; set; }
        public Nullable<int> SemiAnnual1Month { get; set; }
        public Nullable<int> SemiAnnual1Day { get; set; }
        public Nullable<int> SemiAnnual2Month { get; set; }
        public Nullable<int> SemiAnnual2Day { get; set; }
        public Nullable<int> AnnualMOY { get; set; }
        public Nullable<int> AnnualDOM { get; set; }
        public bool DateRangeReq { get; set; }
    }
}
