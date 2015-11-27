using System;
using System.ComponentModel.DataAnnotations;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Debits.MetaData
{
    public class DebitMetadata
    {
        [Required(ErrorMessage = "Please enter : Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter : Amount")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter : Period")]
        [Display(Name = "Period")]
        public int FkPeriod { get; set; }

        [Display(Name = "Begin Date")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Weekly: Weekday")]
        public int? WeeklyDOW { get; set; }

        //[Required(ErrorMessage = @"Please enter : Weekday")]
        //public Weekday? Weekday { get; set; }

        [Display(Name = "Ever Other Week")]
        public int EverOtherWeekDOW { get; set; }

        [Display(Name = "Bi-Monthly Day 1")]
        public int BiMonthlyDay1 { get; set; }

        [Display(Name = "Bi-Monthly Day 2")]
        public int BiMonthlyDay2 { get; set; }

        [Display(Name = "Monthly")]
        public int MonthlyDOM { get; set; }

        [Display(Name = "First Quarter: Month")]
        public int Quarterly1Month { get; set; }

        [Display(Name = "First Quarter: Day")]
        public int Quarterly1Day { get; set; }

        [Display(Name = "Second Quarter: Month")]
        public int Quarterly2Month { get; set; }

        [Display(Name = "Second Quarter: Day")]
        public int Quarterly2Day { get; set; }

        [Display(Name = "Third Quarter: Month")]
        public int Quarterly3Month { get; set; }

        [Display(Name = "Third Quarter: Day")]
        public int Quarterly3Day { get; set; }

        [Display(Name = "Fourth Quarter: Month")]
        public int Quarterly4Month { get; set; }

        [Display(Name = "Fourth Quarter: Day")]
        public int Quarterly4Day { get; set; }

        [Display(Name = "First Semi-Annual: Month")]
        public int SemiAnnual1Month { get; set; }

        [Display(Name = "First Semi-Annual: Day")]
        public int SemiAnnual1Day { get; set; }

        [Display(Name = "Second Semi-Annual: Month")]
        public int SemiAnnual2Month { get; set; }

        [Display(Name = "Second Semi-Annual: Day")]
        public int SemiAnnual2Day { get; set; }

        [Display(Name = "Annual: Month")]
        public int AnnualMOY { get; set; }

        [Display(Name = "Annual: Day")]
        public int AnnualDOM { get; set; }

        [Display(Name = "Date Range")]
        public bool DateRangeReq { get; set; }

        [Display(Name = "Period")]
        public Period Period { get; set; }
    }
}